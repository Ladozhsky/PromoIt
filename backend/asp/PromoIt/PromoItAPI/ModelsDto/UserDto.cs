using System;
using System.Collections.Generic;

namespace PromoItAPI.ModelsDto
{
    public partial class UserDto
    {

        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Address { get; set; }
        public int TelNumber { get; set; }
        public int RoleId { get; set; }
        public int? CompanyId { get; set; }

    }
}

