namespace PromoItBlazorServer.Data
{
    public partial class OrderDto
    {
        public int CampaignId { get; set; }
        public int CompanyId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
    }

    public partial class UpdateQuantity
    {
        public int Quantity { get; set; }
    }
}
