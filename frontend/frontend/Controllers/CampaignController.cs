using frontend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace frontend.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CampaignController : ControllerBase
	{
		private static List<Campaign> Campaigns = new List<Campaign>
		{
			new Campaign
			{
				CampaignId = 1,
				CapmpaignName = "SaveWhales",
				Hashtag = "#SaveWhales",
				Description = "SaveWhales",
				UserId = 1,
				CompanyId = 1,
				ImageUrl = "https://files.worldwildlife.org/wwfcmsprod/images/Humpback_Whale_and_Calf_WW2131047/story_full_width/8q90217t58_Humpback_Whale_and_Calf_WW2131047.jpg"
			},
			new Campaign
			{
				CampaignId = 2,
				CapmpaignName = "SaveLions",
				Hashtag = "#SaveLions",
				Description = "SaveLions",
				UserId = 2,
				CompanyId = 2,
				ImageUrl = "https://cdn.mos.cms.futurecdn.net/FVqUjfbiHS9imyJiRiM53-1200-80.jpg"
			},
			new Campaign
			{
				CampaignId = 3,
				CapmpaignName = "SaveBears",
				Hashtag = "#SaveBears",
				Description = "SaveBears",
				UserId = 3,
				CompanyId = 3,
				ImageUrl = "https://cdn.britannica.com/41/156441-050-A4424AEC/Grizzly-bear-Jasper-National-Park-Canada-Alberta.jpg"
			}
		};
		[HttpGet]
		public async Task<IActionResult> GetCampain()
		{
			return Ok(Campaigns);
		}

	};
}
