using System;
using System.Collections.Generic;

namespace APIPRromoIt.Models
{
    public partial class UserBalance
    {
        public int BalanceId { get; set; }
        public string UserId { get; set; } = null!;
        public int Balance { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
