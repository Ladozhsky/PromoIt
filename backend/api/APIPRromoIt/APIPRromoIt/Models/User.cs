using System;
using System.Collections.Generic;

namespace APIPRromoIt.Models
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
            UserBalances = new HashSet<UserBalance>();
        }

        public string UserId { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Address { get; set; }
        public string TelNumber { get; set; } = null!;
        public string Role { get; set; } = null!;
        public int? CompanyId { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<UserBalance> UserBalances { get; set; }
    }
}
