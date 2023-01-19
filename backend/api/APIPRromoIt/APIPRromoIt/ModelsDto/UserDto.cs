using System.Reflection.Metadata;

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

	public partial class UserForAdmin
	{
		public string UserId { get; set; }
		public string UserName { get; set; } = null!;
        public string EmailTwitterId { get; set; } = null!;
        public string? Address { get; set; }
		public string TelNumber { get; set; } = null!;
		public string Role { get; set; }
		public string? CompanyName { get; set; }
		public string Status { get; set; }
	}
}
