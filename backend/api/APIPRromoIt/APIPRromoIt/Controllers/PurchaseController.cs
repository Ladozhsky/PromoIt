﻿using APIPRromoIt.Models;
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

        // Post new transaction after purchase
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<BalanceTransactionDto>> PostBalanceTransaction(BalanceTransactionDto balanceTransactionDto)
        {
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
        [Authorize]
        public async Task<ActionResult<DonatedProduct>> PostDonatedProduct(DonatedProductDto donatedProductDto)
        {
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

        // Update Status or donation
        [HttpPut("{orderId}")]
        [Authorize]
        public async Task<ActionResult<Donation>> UpdateAmount(int orderId)
        {
            var currentDonation = _context.ProductToOrders.Single(p => p.OrderId == orderId);
            currentDonation.Status = 2;
            _context.SaveChanges();
            return Ok();
        }
    }
}