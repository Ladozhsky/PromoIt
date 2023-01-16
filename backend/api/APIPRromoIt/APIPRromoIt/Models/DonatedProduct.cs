using System;
using System.Collections.Generic;

namespace APIPRromoIt.Models
{
    public partial class DonatedProduct
    {
        public int DonatedProductId { get; set; }
        public int ProductId { get; set; }
        public int CampaignId { get; set; }
        public string UserId { get; set; } = null!;
        public int Amount { get; set; }

        public virtual Campaign Campaign { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
