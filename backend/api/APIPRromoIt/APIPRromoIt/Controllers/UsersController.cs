using APIPRromoIt.Models;
using APIPRromoIt.ModelsDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Linq;

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

        // Get all users
        [HttpGet("/allUsers")]
        //[Authorize(Policy="AdminOnly")]
		public async Task<ActionResult<IEnumerable<UserForAdmin>>> GetAllUsers()
		{
			var users = await (from u in _context.Users
								   join c in _context.Companies on u.CompanyId equals c.CompanyId
								   select new { u.UserName, u.Address, u.TelNumber, c.CompanyName, u.Role, u.Status }).ToListAsync();

			return Ok(users);
		}

		// Add user
		[HttpPost]
        [Authorize]
        public async Task<ActionResult<User>> PostUser(UserDto userDto)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string userId = identity?.FindFirst("user_id")?.Value;
            //string role = identity?.FindFirst("https://promoit.co.il/claims/role")?.Value;
            string email = identity?.FindFirst("https://promoit.co.il/claims/email")?.Value;
            string twitterId = userId.Split('|')[1];

            User user = new User
            {
                UserId = userId,
                UserName = userDto.UserName,
                EmailTwitterId = (email == null) ? twitterId : email,
                Address = userDto.Address,
                TelNumber = userDto.TelNumber,
                Role = userDto.Role,
                CompanyId = userDto.CompanyId,
                Status = "New"
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

        // Get sum of tweets/retweets by campaignId and twitterId
        [HttpGet("/sum")]
        [Authorize(Policy = "Social Activist")]
        public async Task<ActionResult<IEnumerable<DollarsByUser>>> GetTweetsSum()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string userId = identity?.FindFirst("user_id")?.Value;
            string twitterId = userId.Split('|')[1];

            var dollarsByCampaign = await (from bt in _context.BalanceTransactions
                                          join c in _context.Campaigns on bt.CampaignId equals c.CampaignId
                                          where bt.UserId == twitterId
                                           group bt by c.CampaignName into g
                                          select new { CampaignName = g.Key, Dollars = g.Sum(x => x.Amount) }).ToArrayAsync();
            
            return Ok(dollarsByCampaign);
        }

        //Get Roles
        [HttpGet("roles")]
        //[Authorize]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }
    }
}
