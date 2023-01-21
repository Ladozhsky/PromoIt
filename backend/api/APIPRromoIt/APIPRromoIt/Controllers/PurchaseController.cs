using APIPRromoIt.Models;
using APIPRromoIt.ModelsDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using System.Security.Claims;

namespace APIPRromoIt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly promoitContext _context;
        private readonly ILogger<PurchaseController> _logger;

        public PurchaseController(promoitContext context, ILogger<PurchaseController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Get sum of tweets/retweets by campaignId and twitterId
        [HttpGet("{campaignId}")]
        [Authorize(Policy = "Social Activist")]
        public async Task<int> GetTweetsSum(int campaignId)
        {
            _logger.LogInformation("getting sum of tweets");

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string userId = identity?.FindFirst("user_id")?.Value;
            string twitterId = userId.Split('|')[1];

            var tweetsByCampaign = await (from bt in _context.BalanceTransactions
                                          where bt.UserId == twitterId && bt.CampaignId == campaignId
                                             select bt.Amount).ToArrayAsync();
            int tweetsSumByCampaign = tweetsByCampaign.Sum();
            return tweetsSumByCampaign;
        }

        // Post new transaction after purchase
        [HttpPost]
        [Authorize(Policy = "Social Activist")]
        public async Task<ActionResult<BalanceTransactionDto>> PostBalanceTransaction(BalanceTransactionDto balanceTransactionDto)
        {
            _logger.LogInformation("Adding new transaction");

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string userId = identity?.FindFirst("user_id")?.Value;
            string twitterId = userId.Split('|')[1];

            BalanceTransaction balanceTransaction = new BalanceTransaction
            {
                UserId = twitterId,
                CampaignId = balanceTransactionDto.CampaignId,
                Amount = -balanceTransactionDto.Amount,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                CreateByUser = userId,
                UpdateByUser = userId
            };

            _context.BalanceTransactions.Add(balanceTransaction);
            await _context.SaveChangesAsync();

            return Ok(balanceTransaction);
        }

        //Post donated product by user
        [HttpPost("api/Add-donated-product")]
        [Authorize(Policy = "Social Activist")]
        public async Task<ActionResult<DonatedProduct>> PostDonatedProduct(DonatedProductDto donatedProductDto)
        {
            _logger.LogInformation("Adding donated products");

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string userId = identity?.FindFirst("user_id")?.Value;

            DonatedProduct donatedProduct = new DonatedProduct
            {
                ProductId = donatedProductDto.ProductId,
                CampaignId = donatedProductDto.CampaignId,
                UserId = userId,
                Amount = donatedProductDto.Amount
            };

            _context.DonatedProducts.Add(donatedProduct);
            await _context.SaveChangesAsync();

            return Ok(donatedProduct);
        }

        [HttpPost("complete-transaction")]
        [Authorize(Policy="Social Activist")]
        public async Task<ActionResult<CompleteTransaction>> AddCompleteTransaction(CompleteTransactionDto completeTransactionDto)
        {
            _logger.LogInformation("Adding cemplete transaction");

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string userId = identity?.FindFirst("user_id")?.Value;

            CompleteTransaction completeTransaction = new CompleteTransaction
            {
                UserId = userId,
                CampaignId = completeTransactionDto.CampaignId,
                CompanyId = completeTransactionDto.CompanyId,
                ProductId = completeTransactionDto.ProductId,
                Amount = completeTransactionDto.Amount
            };

            _context.CompleteTransactions.Add(completeTransaction);
            await _context.SaveChangesAsync();

            return Ok(completeTransaction);
        }

        // Update Status or donation
        [HttpPut("{orderId}")]
        [Authorize(Policy = "Social Activist")]
        public async Task<ActionResult<Donation>> UpdateAmount(int orderId)
        {
            _logger.LogInformation("Updating donation status");

            var currentDonation = _context.ProductToOrders.Single(p => p.OrderId == orderId);
            currentDonation.Status = 2;
            _context.SaveChanges();
            return Ok();
        }
    }
}
