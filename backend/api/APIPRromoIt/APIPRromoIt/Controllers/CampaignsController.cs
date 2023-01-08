using APIPRromoIt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIPRromoIt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignsController : ControllerBase
    {
        private readonly promoitContext _context;

        public CampaignsController(promoitContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CampaignDto>>> GetCampaigns()
        {
            return await _context.Campaigns.Select(c => CampaignToTDO(c)).ToListAsync();
        }

        private static CampaignDto CampaignToTDO(Campaign campaign) =>
        new CampaignDto
       {
           CampaignId = campaign.CampaignId,
           CampaignName = campaign.CampaignName,
           Hashtag = campaign.Hashtag,
           Description = campaign.Description,
           UserId = campaign.UserId,
           CreateDate = campaign.CreateDate,
           CompanyId = campaign.CompanyId
       };
    }
}
