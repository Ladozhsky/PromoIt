using APIPRromoIt.Models;

namespace APIPRromoIt.ModelsDto
{
    public partial class DonatedProductDto
    {
        public int DonatedProductId { get; set; }
        public int ProductId { get; set; }
        public int CampaignId { get; set; }
        public string UserId { get; set; } = null!;
        public int Amount { get; set; }

    }
}
