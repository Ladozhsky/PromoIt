namespace APIPRromoIt.Models
{
    public partial class CampaignDto
    {

        public int CampaignId { get; set; }
        public string CampaignName { get; set; } = null!;
        public string Hashtag { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreateDate { get; set; }

    }
}
