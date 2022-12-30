using System;
using System.Collections.Generic;

namespace PromoItAPI.Models
{
    public partial class Company
    {
        public Company()
        {
            Campaigns = new HashSet<Campaign>();
            Orders = new HashSet<Order>();
            Products = new HashSet<Product>();
        }

        public int CompanyId { get; set; }
        public string CompanyName { get; set; } = null!;
        public string Site { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string CompanyType { get; set; } = null!;

        public virtual ICollection<Campaign> Campaigns { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
