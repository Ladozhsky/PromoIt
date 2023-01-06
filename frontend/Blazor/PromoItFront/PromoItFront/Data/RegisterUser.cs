namespace PromoItFront.Data
{
    public partial class RegisterUser
    {

        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Address { get; set; }
        public string TelNumber { get; set; }
        public int RoleId { get; set; }

    }
}
