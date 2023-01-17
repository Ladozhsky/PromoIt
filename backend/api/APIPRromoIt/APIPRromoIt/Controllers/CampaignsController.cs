using APIPRromoIt.Models;
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
    public class CampaignsController : ControllerBase
    {
        private readonly promoitContext _context;

        public CampaignsController(promoitContext context)
        {
            _context = context;
        }

        // Get all campaigns
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CampaignDto>>> GetCampaigns()
        {
            var campaigns = await (from ca in _context.Campaigns
                                  join c in _context.Companies on ca.CompanyId equals c.CompanyId
                                  select new { ca.CampaignId, ca.CampaignName, ca.Hashtag, ca.Description, c.CompanyName, ca.CreateDate }).ToListAsync();

            return Ok(campaigns);
        }

        // Get campaign by user id
        [HttpGet("/api/Campaigns/byUser")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CampaignDto>>> GetCampaignsByUserId()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string userId = identity?.FindFirst("user_id")?.Value;

            var campaigns = await _context.Campaigns
            .Where(c => c.UserId == userId)
            .ToListAsync();

            var campaignDtos = campaigns.Select(CampaignToTDO);

            return Ok(campaignDtos);
        }

        // Get campaign by id
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<CampaignDto>> GetCampaign(int id)
        {
            var campaign = await _context.Campaigns.FindAsync(id);

            if (campaign == null)
            {
                return NotFound();
            }
            return CampaignToTDO(campaign);
        }

        // Add campaign
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CampaignDto>> PostCampaign(CampaignDto campaignDto)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string userId = identity?.FindFirst("user_id")?.Value;

            var companyId = await _context.Users.Where(x => x.UserId == userId)
                                            .Select(x => x.CompanyId)
                                            .FirstOrDefaultAsync();

            Campaign campaign = new Campaign
            {
                CampaignId = campaignDto.CampaignId,
                CampaignName = campaignDto.CampaignName,
                Hashtag = campaignDto.Hashtag,
                Description = campaignDto.Description,
                UserId = userId,
                CreateDate = DateTime.Now,
                CompanyId = (int)companyId
            };

            _context.Campaigns.Add(campaign);
            await _context.SaveChangesAsync();

            return Ok(campaign);
        }

        // Update campaign
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutCampaign(int id, CampaignDto campaignDto)
        {
            if (id != campaignDto.CampaignId)
            {
                return BadRequest();
            }

            var campaign = await _context.Campaigns.FindAsync(id);
            if (campaign == null)
            {
                return NotFound();
            }

            campaign.CampaignName = campaignDto.CampaignName;
            campaign.Hashtag = campaignDto.Hashtag;
            campaign.Description = campaignDto.Description;
            //campaign.UserId = campaignDto.UserId;
            //campaign.CreateDate = campaignDto.CreateDate;
            //campaign.CompanyId = campaignDto.CompanyId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!CampaignExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // Delete campaign
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteCampaign(int id)
        {
            var campaign = await _context.Campaigns.FindAsync(id);
            if (campaign == null)
            {
                return NotFound();
            }

            _context.Campaigns.Remove(campaign);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CampaignExists(int id)
        {
            return _context.Campaigns.Any(e => e.CampaignId == id);
        }

        private static CampaignDto CampaignToTDO(Campaign campaign) =>
        new CampaignDto
       {
           CampaignId = campaign.CampaignId,
           CampaignName = campaign.CampaignName,
           Hashtag = campaign.Hashtag,
           Description = campaign.Description,
           //CompanyId = campaign.CompanyId,
           CreateDate = DateTime.Now.Date
       };
    }
}
