using APIPRromoIt.Models;
using APIPRromoIt.ModelsDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Linq;
using System.Collections.Generic;

namespace APIPRromoIt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly promoitContext _context;
        private readonly ILogger<UsersController> _logger;

        public UsersController(promoitContext context, ILogger<UsersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Get all users
        [HttpGet("/allUsers")]
        [Authorize(Policy="AdminOnly")]
		public async Task<ActionResult<IEnumerable<UserForAdmin>>> GetAllUsers()
		{
            _logger.LogInformation("Getting Users");

			var users = await (from u in _context.Users
								   join c in _context.Companies on u.CompanyId equals c.CompanyId
                                   orderby u.Status descending
                                   select new { u.UserId, u.UserName, u.EmailTwitterId, u.Address, u.TelNumber, c.CompanyName, u.Role, u.Status }).ToListAsync();

			return Ok(users);
		}

		// Add user
		[HttpPost]
        [Authorize]
        public async Task<ActionResult<User>> PostUser(UserDto userDto)
        {
            _logger.LogInformation("Adding user");

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
            _logger.LogInformation("UserExisting checking");

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
            _logger.LogInformation("getting sum of tweets");

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
        [Authorize]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            _logger.LogInformation("Getting roles");

            IEnumerable<Role> roles = await _context.Roles.ToListAsync();
            return Ok(roles);
        }

        // Update Status of User
        [HttpPut("{userId}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<User>> UpdateUserStatus(string userId)
        {
            _logger.LogInformation("Updating user status");

            var currentUser = _context.Users.Single(u => u.UserId == userId);
            currentUser.Status = "Verified";
            _context.SaveChanges();
            return Ok();
        }
    }
}
