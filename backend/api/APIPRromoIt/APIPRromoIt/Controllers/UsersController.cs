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
        //[Authorize]
        public async Task<ActionResult<User>> PostUser(UserDto userDto)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userId = identity?.FindFirst("user_id")?.Value;
            var role = identity?.FindFirst("https://promoit.co.il/claims/role")?.Value;
            var email = identity?.FindFirst("https://promoit.co.il/claims/email")?.Value;

            var user = new User
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
            //return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, UserToTDO(user));
        }

        //public async Task<ActionResult<UserDto>> GetUser(string id)
        //{
        //    var user = await _context.Users.FindAsync(id);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    return UserToTDO(user);
        //}

        //private static UserDto UserToTDO(User user) =>
        //new UserDto
        //{
        //    UserId = user.UserId,
        //    UserName = user.UserName,
        //    Email = user.Email,
        //    Address = user.Address,
        //    TelNumber = user.TelNumber,
        //    Role = user.Role,
        //    CompanyId = user.CompanyId
        //};
    }
}
