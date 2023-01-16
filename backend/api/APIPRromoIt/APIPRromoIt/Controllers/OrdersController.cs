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

        public OrdersController(promoitContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Donation>>> GetDonations()
        {
            var donations = await (from ca in _context.Campaigns
                                   join o in _context.Orders on ca.CampaignId equals o.CampaignId
                                   join c in _context.Companies on o.CompanyId equals c.CompanyId
                                   join po in _context.ProductToOrders on o.OrderId equals po.OrderId
                                   join p in _context.Products on po.ProductId equals p.ProductId
                                   select new { ca.CampaignId, ca.CampaignName, c.CompanyName, p.ProductName, p.ProductId, p.Price, po.Amount, ca.Hashtag
        }).ToListAsync();

            return Ok(donations);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<OrderDto>> PostOrder(OrderDto orderDto)
        {
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

            ProductToOrder orderToProduct = new ProductToOrder
            {
                OrderId = order.OrderId,
                ProductId = orderDto.ProductId,
                Amount = orderDto.Amount,
            };

            _context.ProductToOrders.Add(orderToProduct);
            await _context.SaveChangesAsync();

            return Ok(orderDto);
        }
    }
}
