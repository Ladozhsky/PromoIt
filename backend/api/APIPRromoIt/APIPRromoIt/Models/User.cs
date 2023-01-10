using System;
using System.Collections.Generic;

namespace APIPRromoIt.Models
{
    public partial class User
    {
        public User()
        {
            //Campaigns = new HashSet<Campaign>();
            Orders = new HashSet<Order>();
            UserBalances = new HashSet<UserBalance>();
        }

        public string UserId { get; set; }
        public string UserName { get; set; } = null!;
        //public string PasswordHash { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Address { get; set; }
        public string TelNumber { get; set; } = null!;
        public int Role { get; set; }
        public int? CompanyId { get; set; }

        //public virtual Role Role { get; set; } = null!;
        //public virtual ICollection<Campaign> Campaigns { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<UserBalance> UserBalances { get; set; }
    }
}
