using APIPRromoIt.Models;
using APIPRromoIt.ModelsDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace APIPRromoIt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly promoitContext _context;

        public UsersController(promoitContext context)
        {
            _context = context;
        }

        // Add user
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<User>> PostUser(UserDto userDto)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string userId = identity?.FindFirst("user_id")?.Value;
            string role = identity?.FindFirst("https://promoit.co.il/claims/role")?.Value;
            string email = identity?.FindFirst("https://promoit.co.il/claims/email")?.Value;

            User user = new User
            {
                UserId = userId,
                UserName = userDto.UserName,
                Email = email,
                Address = userDto.Address,
                TelNumber = userDto.TelNumber,
                Role = role,
                CompanyId = userDto.CompanyId
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpGet]
        public async Task<ActionResult<bool>> UserExisting()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string userId = identity?.FindFirst("user_id")?.Value;

            User existingUser = _context.Users.FirstOrDefault(u => u.UserId == userId);
            if (existingUser != null)
            {
                return true;
            }
            return false;
        }
    }
}
