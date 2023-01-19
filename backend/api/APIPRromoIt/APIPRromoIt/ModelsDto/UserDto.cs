namespace APIPRromoIt.ModelsDto
{
    public partial class UserDto
    {
        public string UserName { get; set; } = null!;
        public string? Address { get; set; }
        public string TelNumber { get; set; } = null!;
        public string Role { get; set; }
        public int? CompanyId { get; set; }
    }
}
