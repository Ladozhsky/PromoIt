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
    public class ProductsController : ControllerBase
    {
        private readonly promoitContext _context;

        public ProductsController(promoitContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ProductDto>> PostProduct(ProductDto productDto)
        {
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
        [Authorize]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByCompanyId()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string userId = identity?.FindFirst("user_id")?.Value;

            var companyId = await _context.Users.Where(x => x.UserId == userId)
                                            .Select(x => x.CompanyId)
                                            .FirstOrDefaultAsync();

            var products = await (from p in _context.Products
                            join c in _context.Companies on p.CompanyId equals c.CompanyId
                            where c.CompanyId == companyId
                            select new { p.ProductName, p.Price, p.Image, c.CompanyName }).ToListAsync();

            return Ok(products);
        }
    }
}
