namespace PromoItBlazorServer.Data
{
    public partial class UserForAdmin
    {
        public string UserName { get; set; } = null!;
        public string? Address { get; set; }
        public string TelNumber { get; set; } = null!;
        public string Role { get; set; }
        public string? CompanyName { get; set; }
        public string Status { get; set; }
    }
}
