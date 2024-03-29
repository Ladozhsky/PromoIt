﻿using APIPRromoIt.Models;
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
    public class ProductsController : ControllerBase
    {
        private readonly promoitContext _context;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(promoitContext context, ILogger<ProductsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        [Authorize(Policy = "Business representative and Admin")]
        public async Task<ActionResult<ProductDto>> PostProduct(ProductDto productDto)
        {
            _logger.LogInformation("Adding product");

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string userId = identity?.FindFirst("user_id")?.Value;

            var companyId = await _context.Users.Where(x => x.UserId == userId)
                                            .Select(x => x.CompanyId)
                                            .FirstOrDefaultAsync();

            Product product = new Product
            {
                ProductId = productDto.ProductId,
                ProductName = productDto.ProductName,
                Price = productDto.Price,
                CompanyId = (int)companyId,
                Image = productDto.Image
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return Ok(product);
        }

        [HttpGet("/api/Products/byCompany")]
        [Authorize(Policy ="Business Representative")]
        public async Task<ActionResult<List<ProductDto>>> GetProductsByCompanyId()
        {
            _logger.LogInformation("Getting products by company");

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string userId = identity?.FindFirst("user_id")?.Value;

            var companyId = await _context.Users.Where(x => x.UserId == userId)
                                            .Select(x => x.CompanyId)
                                            .FirstOrDefaultAsync();

            var products = await (from p in _context.Products
                            join c in _context.Companies on p.CompanyId equals c.CompanyId
                            where c.CompanyId == companyId
                            select new { p.ProductId, p.ProductName, p.Price, p.Image, c.CompanyName }).ToListAsync();

            return Ok(products);
        }

        [HttpGet("/api/donated-products")]
        [Authorize(Policy = "Social Activist")]
        public async Task<ActionResult<List<DonatedProductDto>>> GetDonatedProductsByUserId()
        {
            _logger.LogInformation("Getting donated products");

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string userId = identity?.FindFirst("user_id")?.Value;

            var donatedProducts = await (from dp in _context.DonatedProducts
                                  join c in _context.Campaigns on dp.CampaignId equals c.CampaignId
                                  join p in _context.Products on dp.ProductId equals p.ProductId
                                  where dp.UserId == userId
                                  select new { p.ProductName, c.CampaignName, dp.Amount }).ToListAsync();

            return Ok(donatedProducts);
        }

        //Checking of existing the same donated product
        [HttpGet("{productId}/{campaignId}")]
        [Authorize(Policy = "Social Activist")]
        public async Task<ActionResult<bool>> DonatedProductExisting(int productId, int campaignId)
        {
            _logger.LogInformation("Donated product existing checking");

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string userId = identity?.FindFirst("user_id")?.Value;

            DonatedProduct existingDonatedProduct = _context.DonatedProducts.FirstOrDefault(dp => dp.UserId == userId && dp.ProductId == productId && dp.CampaignId == campaignId);
            if (existingDonatedProduct != null)
            {
                return true;
            }
            return false;
        }

        // Update amount of donated products
        [HttpPut("{productId}/{campaignId}")]
        [Authorize(Policy = "Social Activist")]
        public async Task<ActionResult<DonatedProduct>> UpdateAmount(UpdateQuantity updateQuantity, int productId, int campaignId)
        {
            _logger.LogInformation("Updating donation status");

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string userId = identity?.FindFirst("user_id")?.Value;

            var donatedProduct = _context.DonatedProducts.Single(dp => dp.UserId == userId && dp.ProductId == productId && dp.CampaignId == campaignId);
            donatedProduct.Amount += updateQuantity.Quantity;
            _context.SaveChanges();
            return Ok();
        }
    }
}
