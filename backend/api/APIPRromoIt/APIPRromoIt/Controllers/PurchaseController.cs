using APIPRromoIt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace APIPRromoIt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly promoitContext _context;

        public PurchaseController(promoitContext context)
        {
            _context = context;
        }

        // Get sum of tweets/retweets by campaignId and twitterId
        [HttpGet("{campaignId}")]
        [Authorize]
        public async Task<int> GetTweetsSum(int campaignId)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string userId = identity?.FindFirst("user_id")?.Value;
            string twitterId = userId.Split('|')[1];

            var tweetsByCampaign = await (from bt in _context.BalanceTransactions
                                          where bt.UserId == twitterId && bt.CampaignId == campaignId
                                             select bt.Amount).ToArrayAsync();
            int tweetsSumByCampaign = tweetsByCampaign.Sum();
            return tweetsSumByCampaign;
        }
    }
}
