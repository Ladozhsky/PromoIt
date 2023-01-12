using System;
using System.Collections.Generic;

namespace APIPRromoIt.Models
{
    public partial class Company
    {
        public Company()
        {
            Campaigns = new HashSet<Campaign>();
            Orders = new HashSet<Order>();
            Products = new HashSet<Product>();
            Users = new HashSet<User>();
        }

        public int CompanyId { get; set; }
        public string CompanyName { get; set; } = null!;
        public string Site { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string CompanyType { get; set; }

        public virtual ICollection<Campaign> Campaigns { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
