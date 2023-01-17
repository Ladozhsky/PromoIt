using System;
using System.Collections.Generic;

namespace APIPRromoIt.Models
{
    public partial class Order
    {
        public Order()
        {
            ProductToOrders = new HashSet<ProductToOrder>();
        }

        public int OrderId { get; set; }
        public int CampaignId { get; set; }
        public int CompanyId { get; set; }
        public string UserId { get; set; } = null!;
        public int? Quantity { get; set; }

        public virtual Campaign Campaign { get; set; } = null!;
        public virtual Company Company { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<ProductToOrder> ProductToOrders { get; set; }
    }
}
