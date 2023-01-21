using APIPRromoIt.Models;

namespace APIPRromoIt.ModelsDto
{
    public partial class OrderDto
    {
        public int CampaignId { get; set; }
        public int CompanyId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
    }

    public partial class Donation
    {
        public string CampaignName { get; set; }
        public int CampaignId { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string ProductName { get; set; }
        public int Amout { get; set; }
        public string Hashtag { get; set; }
        public int Quantity { get; set; }
        public int OrderId { get; set; }
    }

    public partial class UpdateQuantity
    {
        public int Quantity { get; set; }
    }

    public partial class UpdateStatus
    {
        public int Status { get; set; }
    }
}
