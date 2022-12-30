using System;
using System.Collections.Generic;

namespace PromoItAPI.Models
{
    public partial class UserBalance
    {
        public int BalanceId { get; set; }
        public int UserId { get; set; }
        public int Balance { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
