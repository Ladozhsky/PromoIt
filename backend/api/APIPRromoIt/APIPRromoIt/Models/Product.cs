using System;
using System.Collections.Generic;

namespace APIPRromoIt.Models
{
    public partial class Product
    {
        public Product()
        {
            CompleteTransactions = new HashSet<CompleteTransaction>();
            DonatedProducts = new HashSet<DonatedProduct>();
            ProductToOrders = new HashSet<ProductToOrder>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int Price { get; set; }
        public int CompanyId { get; set; }
        public string? Image { get; set; }

        public virtual Company Company { get; set; } = null!;
        public virtual ICollection<CompleteTransaction> CompleteTransactions { get; set; }
        public virtual ICollection<DonatedProduct> DonatedProducts { get; set; }
        public virtual ICollection<ProductToOrder> ProductToOrders { get; set; }
    }
}
