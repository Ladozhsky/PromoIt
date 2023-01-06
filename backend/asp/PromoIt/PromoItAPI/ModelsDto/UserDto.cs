using System;
using System.Collections.Generic;

namespace PromoItAPI.ModelsDto
{
    public partial class UserDto
    {

        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Address { get; set; }
        public string TelNumber { get; set; }
        public int RoleId { get; set; }

    }
}

