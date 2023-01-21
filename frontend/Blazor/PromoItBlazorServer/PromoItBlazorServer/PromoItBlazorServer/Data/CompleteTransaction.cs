namespace PromoItBlazorServer.Data
{
    public partial class CompleteTransaction
    {
        public int CampaignId { get; set; }
        public int CompanyId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
    }

    public partial class CompleteTransactionsForAdmin
    {
        public string UserName { get; set; }
        public string Address { get; set; }
        public string TelNumber { get; set; }
        public string CampaignName { get; set; }
        public string CompanyName { get; set; }
        public string ProductName { get; set; }
        public int Amount { get; set; }
    }
}
