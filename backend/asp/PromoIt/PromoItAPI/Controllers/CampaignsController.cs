using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PromoItAPI.Models;
using PromoItAPI.ModelsDto;

namespace PromoItAPI.Controllers
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

        // GET: api/Campaigns
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Campaign>>> GetCampaigns()
        //{
        //    return await _context.Campaigns.ToListAsync();
        //}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<CampaignDto>>> GetCampaigns()
		{
			return await _context.Campaigns.Select(c => CampaignToTDO(c)).ToListAsync();

		}

		// GET: api/Campaigns/5
		//[HttpGet("{id}")]
		//public async Task<ActionResult<Campaign>> GetCampaign(int id)
		//{
		//    var campaign = await _context.Campaigns.FindAsync(id);

		//    if (campaign == null)
		//    {
		//        return NotFound();
		//    }

		//    return campaign;
		//}

		// get campaigns by user id

		[HttpGet("/api/Campaigns/byUser/{userId}")]
		public async Task<ActionResult<IEnumerable<CampaignDto>>> GetCampaignsByUserId(int userId)
		{
			var campaigns = await _context.Campaigns
			.Where(c => c.UserId == userId)
			.ToListAsync();

			var campaignDtos = campaigns.Select(CampaignToTDO);

			return Ok(campaignDtos);
		}

			// get campaign

			[HttpGet("{id}")]
		public async Task<ActionResult<CampaignDto>> GetCampaign(int id)
		{
			var campaign = await _context.Campaigns.FindAsync(id);

			if (campaign == null)
			{
				return NotFound();
			}

			return CampaignToTDO(campaign);
		}

		// PUT: api/Campaigns/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		//[HttpPut("{id}")]
		//      public async Task<IActionResult> PutCampaign(int id, Campaign campaign)
		//      {
		//          if (id != campaign.CampaignId)
		//          {
		//              return BadRequest();
		//          }

		//          _context.Entry(campaign).State = EntityState.Modified;

		//          try
		//          {
		//              await _context.SaveChangesAsync();
		//          }
		//          catch (DbUpdateConcurrencyException)
		//          {
		//              if (!CampaignExists(id))
		//              {
		//                  return NotFound();
		//              }
		//              else
		//              {
		//                  throw;
		//              }
		//          }

		//          return NoContent();
		//      }

		[HttpPut("{id}")]
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
			campaign.UserId = campaignDto.UserId;
			campaign.CreateDate = campaignDto.CreateDate;
			campaign.CompanyId = campaignDto.CompanyId;

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



		// POST: api/Campaigns
		//[HttpPost]
		//      public async Task<ActionResult<Campaign>> PostCampaign(Campaign campaign)
		//      {
		//          _context.Campaigns.Add(campaign);
		//          await _context.SaveChangesAsync();

		//          return CreatedAtAction("GetCampaign", new { id = campaign.CampaignId }, campaign);
		//      }

		[HttpPost]
		public async Task<ActionResult<CampaignDto>> PostCampaign(CampaignDto campaignDto)
		{
			var campaign = new Campaign
			{
				CampaignId = campaignDto.CampaignId,
				CampaignName = campaignDto.CampaignName,
				Hashtag = campaignDto.Hashtag,
				Description = campaignDto.Description,
				UserId = campaignDto.UserId,
				CreateDate = campaignDto.CreateDate,
				CompanyId = campaignDto.CompanyId
			};

			_context.Campaigns.Add(campaign);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetCampaign), new { id = campaign.CampaignId }, CampaignToTDO(campaign));
		}

		// DELETE: api/Campaigns/5
		[HttpDelete("{id}")]
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
           UserId = campaign.UserId,
           CreateDate = campaign.CreateDate,
           CompanyId = campaign.CompanyId
	   };
	}
}
