﻿using System;
using System.Collections.Generic;

namespace APIPRromoIt.Models
{
    public partial class User
    {
        public User()
        {
            Campaigns = new HashSet<Campaign>();
            CompleteTransactions = new HashSet<CompleteTransaction>();
            DonatedProducts = new HashSet<DonatedProduct>();
            Orders = new HashSet<Order>();
            UserBalances = new HashSet<UserBalance>();
        }

        public string UserId { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string EmailTwitterId { get; set; } = null!;
        public string? Address { get; set; }
        public string TelNumber { get; set; } = null!;
        public string Role { get; set; } = null!;
        public int? CompanyId { get; set; }
        public string Status { get; set; } = null!;

        public virtual Company? Company { get; set; }
        public virtual ICollection<Campaign> Campaigns { get; set; }
        public virtual ICollection<CompleteTransaction> CompleteTransactions { get; set; }
        public virtual ICollection<DonatedProduct> DonatedProducts { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<UserBalance> UserBalances { get; set; }
    }
}
