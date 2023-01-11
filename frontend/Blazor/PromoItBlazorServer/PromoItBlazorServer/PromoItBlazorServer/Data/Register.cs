namespace PromoItBlazorServer.Data
{
    public partial class Register
    {
        public string UserName { get; set; } = null!;
        public string? Address { get; set; }
        public string TelNumber { get; set; } = null!;
        public int? CompanyId { get; set; }
    }
}
