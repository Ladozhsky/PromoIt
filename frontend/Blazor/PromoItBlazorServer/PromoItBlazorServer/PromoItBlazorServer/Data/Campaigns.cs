namespace PromoItBlazorServer.Data
{
	public partial class CampaignDto
	{
		public int CampaignId { get; set; }
		public string CampaignName { get; set; } = null!;
		public string Hashtag { get; set; } = null!;
		public string Description { get; set; } = null!;
		public int CompanyId { get; set; }
		public int UserId { get; set; }
		public DateTime CreateDate { get; set; }
		public string CompanyName { get; set; }
	}
}
