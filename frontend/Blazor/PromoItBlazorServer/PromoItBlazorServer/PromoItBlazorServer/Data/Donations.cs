namespace PromoItBlazorServer.Data
{
    public partial class DonationDto
    {
        public int CampaignId { get; set; }
        public string CampaignName { get; set; }
        public string CompanyName { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }
        public string Hashtag { get; set; }
        public int Quantity { get; set; }
        public int OrderId { get; set; }
    }


}
