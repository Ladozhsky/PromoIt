﻿namespace PromoItBlazorServer.Data
{
    public partial class DonationDto
    {
        public string CampaignName { get; set; }
        public string CompanyName { get; set; }
        public string ProductName { get; set; }
        public int Amount { get; set; }
        public string Hashtag { get; set; }
    }
}