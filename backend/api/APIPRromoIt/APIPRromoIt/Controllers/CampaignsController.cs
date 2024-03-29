﻿using APIPRromoIt.Models;
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
        private readonly ILogger<CampaignsController> _logger;

        public CampaignsController(promoitContext context, ILogger<CampaignsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Get all campaigns
        [HttpGet]
        public async Task<ActionResult<List<CampaignDto>>> GetCampaigns()
        {
            _logger.LogInformation("Getting all campaigns");

            var campaigns = await (from ca in _context.Campaigns
                                   join c in _context.Companies on ca.CompanyId equals c.CompanyId
                                   select new { ca.CampaignId, ca.CampaignName, ca.Hashtag, ca.Description, c.CompanyName, ca.CreateDate }).ToListAsync();

            // var campaigns = await _context.Campaigns.Include(c => c.Company).Select(c => CampaignToTDO(c)).ToArrayAsync();

            return Ok(campaigns);
        }

        // Get campaign by user id
        [HttpGet("/api/Campaigns/byUser")]
        [Authorize(Policy = "NPO Representative")]
        public async Task<ActionResult<List<CampaignDto>>> GetCampaignsByUserId()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string userId = identity?.FindFirst("user_id")?.Value;

            _logger.LogInformation($"Getting campaign by userId: {userId}");

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
            _logger.LogInformation($"Getting campaign by id: {id}");

            var campaign = await _context.Campaigns.FindAsync(id);

            if (campaign == null)
            {
                _logger.LogWarning($"Campaign with {id} id not found");
                return NotFound();
            }
            return CampaignToTDO(campaign);
        }

        // Add campaign
        [HttpPost]
        [Authorize(Policy = "NPO Representative")]
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

            _logger.LogInformation($"Adding campaign {campaign}");

            _context.Campaigns.Add(campaign);
            await _context.SaveChangesAsync();

            return Ok(campaign);
        }

        // Update campaign
        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> PutCampaign(int id, CampaignDto campaignDto)
        {
            if (id != campaignDto.CampaignId)
            {
                _logger.LogWarning($"Campaign with {id} doesn't exist");
                return BadRequest();
            }

            var campaign = await _context.Campaigns.FindAsync(id);
            if (campaign == null)
            {
                _logger.LogWarning("Campaign not found");
                return NotFound();
            }

            campaign.CampaignName = campaignDto.CampaignName;
            campaign.Hashtag = campaignDto.Hashtag;
            campaign.Description = campaignDto.Description;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Delete campaign
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteCampaign(int id)
        {
            _logger.LogInformation("Deleting campaign");
            var campaign = await _context.Campaigns.FindAsync(id);
            if (campaign == null)
            {
                _logger.LogWarning($"Could not delete campaign with id: {id}");
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
           CompanyId = campaign.CompanyId,
           CreateDate = DateTime.Now.Date
       };
    }
}
