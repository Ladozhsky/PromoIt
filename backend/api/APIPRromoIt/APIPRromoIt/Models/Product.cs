﻿using System;
using System.Collections.Generic;

namespace APIPRromoIt.Models
{
    public partial class Product
    {
        public Product()
        {
            Orders = new HashSet<Order>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int Price { get; set; }
        public int CompanyId { get; set; }
        public string? Image { get; set; }

        public virtual Company Company { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
    }
}
