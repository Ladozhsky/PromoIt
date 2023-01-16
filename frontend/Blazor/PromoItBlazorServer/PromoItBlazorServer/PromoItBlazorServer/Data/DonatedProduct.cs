namespace PromoItBlazorServer.Data
{
    public partial class DonatedProductDto
    {
        public int DonatedProductId { get; set; }
        public int ProductId { get; set; }
        public int CampaignId { get; set; }
        public string UserId { get; set; } = null!;
        public int Amount { get; set; }

    }

    public partial class DonatedProductList
    {
            public string ProductName { get; set; }
            public string CampaignName { get; set; }
            public int Amount { get; set; }
    }
}
