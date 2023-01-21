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
    public class OrdersController : ControllerBase
    {
        private readonly promoitContext _context;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(promoitContext context, ILogger<OrdersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<Donation>>> GetDonations()
        {
            _logger.LogInformation("Getting donations");

            var donations = await (from ca in _context.Campaigns
                                   join o in _context.Orders on ca.CampaignId equals o.CampaignId
                                   join c in _context.Companies on o.CompanyId equals c.CompanyId
                                   join po in _context.ProductToOrders on o.OrderId equals po.OrderId
                                   join p in _context.Products on po.ProductId equals p.ProductId
                                   where po.Status == 1
                                   select new { ca.CampaignId, ca.CampaignName, c.CompanyName, c.CompanyId, p.ProductName, p.ProductId, p.Price, po.Amount, ca.Hashtag, o.Quantity, o.OrderId
        }).ToListAsync();

            return Ok(donations);
        }

        [HttpPut("{orderId}")]
        [Authorize(Policy ="Social Activist")]
        public async Task<ActionResult<Donation>> UpdateAmount(int orderId, UpdateQuantity updateQuantity)
        {
            _logger.LogInformation("Updating amount");

            var productToOrder = _context.ProductToOrders.Single(p => p.OrderId == orderId);
            productToOrder.Amount -= updateQuantity.Quantity;
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Authorize(Policy ="Business Representative")]
        public async Task<ActionResult<OrderDto>> PostOrder(OrderDto orderDto)
        {
            _logger.LogInformation("Adding order");

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string userId = identity?.FindFirst("user_id")?.Value;

            var companyId = await _context.Users.Where(x => x.UserId == userId)
                                            .Select(x => x.CompanyId)
                                            .FirstOrDefaultAsync();

            Order order = new Order
            {
                CampaignId = orderDto.CampaignId,
                CompanyId = (int)companyId,
                UserId = userId,
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Adding product_to_order");

            ProductToOrder orderToProduct = new ProductToOrder
            {
                OrderId = order.OrderId,
                ProductId = orderDto.ProductId,
                Amount = orderDto.Amount,
                Status = 1
            };

            _context.ProductToOrders.Add(orderToProduct);
            await _context.SaveChangesAsync();

            return Ok(orderDto);
        }
    }
}
