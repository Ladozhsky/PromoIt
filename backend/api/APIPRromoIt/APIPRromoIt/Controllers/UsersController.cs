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
        public async Task<ActionResult<CampaignDto>> PostUser(UserDto userDto)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userId = identity?.FindFirst("https://promoteit.co.il/claims/user_id")?.Value;
            var role = identity?.FindFirst("https://promoteit.co.il/claims/role")?.Value;
            var email = identity?.FindFirst("name")?.Value;

            var user = new User
            {
                UserId = userId,
                UserName = userDto.UserName,
                Email = email,
                Address = userDto.Address,
                TelNumber = userDto.TelNumber,
                CompanyId = userDto.CompanyId
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user);
                //CreatedAtAction(nameof(GetCampaign), new { id = campaign.CampaignId }, CampaignToTDO(campaign));
        }
    }
}
