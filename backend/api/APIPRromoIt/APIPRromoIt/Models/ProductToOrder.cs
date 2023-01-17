using System;
using System.Collections.Generic;

namespace APIPRromoIt.Models
{
    public partial class ProductToOrder
    {
        public int PoId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public int Status { get; set; }

        public virtual Order Order { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
