namespace APIPRromoIt.ModelsDto
{
    public partial class UserDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Address { get; set; }
        public string TelNumber { get; set; } = null!;
        public int Role { get; set; }
        public int? CompanyId { get; set; }
    }
}
