using Tweetinvi.Core.Extensions;
using static System.Net.WebRequestMethods;

namespace PromoItBlazorServer
{
    public class RequestsUrls
    {
        public string Campaigns { get; set; } = "https://localhost:7096/api/Campaigns";
        public string CampaignsByUser { get; set; } = "https://localhost:7096/api/Campaigns/byUser";
        public string Companies { get; set; } = "https://localhost:7096/api/Companies";
        public string Purchase { get; set; } = "https://localhost:7096/api/Purchase";
        public string Products { get; set; } = "https://localhost:7096/api/Products";
        public string NewDonatedProducts { get; set; } = "https://localhost:7096/api/Purchase/api/Add-donated-product";
        public string Orders { get; set; } = "https://localhost:7096/api/Orders";
        public string CompletedTransactions { get; set; } = "https://localhost:7096/api/Purchase/complete-transaction";
        public string Twitter { get; set; } = "http://localhost:5555/twitter/";
        public string ProductsByCompany { get; set; } = "https://localhost:7096/api/Products/byCompany";
        public string DonatedProducts { get; set; } = "https://localhost:7096/api/donated-products";
        public string AllUsers { get; set; } = "https://localhost:7096/allUsers";
        public string User { get; set; } = "https://localhost:7096/api/Users";
        public string Roles { get; set; } = "https://localhost:7096/api/Users/roles";
        public string Dollars { get; set; } = "https://localhost:7096/sum";
        public string Transactions { get; set; } = "https://localhost:7096/api/Users/transactions";
    }
}
