using APIPRromoIt.Models;

namespace APIPRromoIt.ModelsDto
{
    public partial class OrderDto
    {
        public int OrderId { get; set; }
        public int CampaignId { get; set; }
        public int CompanyId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
    }
}
