namespace frontend.Models
{
    public class Campaign
    {
		public int CampaignId { get; set; }
		public string CapmpaignName { get; set; }
		public string Hashtag { get; set; }
		public string Description { get; set; }
		public int UserId { get; set; }
		public int CompanyId { get; set; }

		public string ImageUrl { get; set; }
	}
}