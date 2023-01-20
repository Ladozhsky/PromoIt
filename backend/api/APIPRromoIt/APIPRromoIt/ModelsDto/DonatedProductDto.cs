using APIPRromoIt.Models;

namespace APIPRromoIt.ModelsDto
{
    public partial class DonatedProductDto
    {
        public int ProductId { get; set; }
        public int CampaignId { get; set; }
        public int Amount { get; set; }

    }

    public partial class ExistingDonatedProductDto
    {
        public int ProductId { get; set; }
        public int CampaignId { get; set; }
        public string UserId { get; set; }

    }
}
