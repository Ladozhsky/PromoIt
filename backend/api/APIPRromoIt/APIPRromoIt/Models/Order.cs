using System;
using System.Collections.Generic;

namespace APIPRromoIt.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public int CampaignId { get; set; }
        public int CompanyId { get; set; }
        public int Amount { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }

        public virtual Campaign Campaign { get; set; } = null!;
        public virtual Company Company { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
