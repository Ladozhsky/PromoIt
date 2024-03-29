﻿using System;
using System.Collections.Generic;

namespace APIPRromoIt.Models
{
    public partial class Campaign
    {
        public Campaign()
        {
            BalanceTransactions = new HashSet<BalanceTransaction>();
            CompleteTransactions = new HashSet<CompleteTransaction>();
            DonatedProducts = new HashSet<DonatedProduct>();
            Orders = new HashSet<Order>();
        }

        public int CampaignId { get; set; }
        public string CampaignName { get; set; } = null!;
        public string Hashtag { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<BalanceTransaction> BalanceTransactions { get; set; }
        public virtual ICollection<CompleteTransaction> CompleteTransactions { get; set; }
        public virtual ICollection<DonatedProduct> DonatedProducts { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
