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
    public class CompaniesController : ControllerBase
    {
        private readonly promoitContext _context;
        private readonly ILogger<CompaniesController> _logger;

        public CompaniesController(promoitContext context, ILogger<CompaniesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompanies()
        {
                _logger.LogInformation("Getting companies");

                return await _context.Companies.Select(c => CompanyToTDO(c)).ToListAsync();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Company>> PostCompany(CompanyDto companyDto)
        {
            _logger.LogInformation("Adding company");

            Company company = new Company
            {
                CompanyId = companyDto.CompanyId,
                CompanyName = companyDto.CompanyName,
                Site = companyDto.Site,
                Email = companyDto.Email,
                CompanyType = companyDto.CompanyType
            };

                _context.Companies.Add(company);
                await _context.SaveChangesAsync();

                return Ok(company);
        }
        private static CompanyDto CompanyToTDO(Company company) =>
        new CompanyDto
        {
            CompanyId = company.CompanyId,
            CompanyName = company.CompanyName,
            Site = company.Site,
            Email = company.Email,
            CompanyType = company.CompanyType
        };
    }
}
