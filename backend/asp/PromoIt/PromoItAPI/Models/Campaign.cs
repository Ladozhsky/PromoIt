using System;
using System.Collections.Generic;

namespace PromoItAPI.Models
{
    public partial class Campaign
    {
        public Campaign()
        {
            Orders = new HashSet<Order>();
        }

        public int CampaignId { get; set; }
        public string CampaignName { get; set; } = null!;
        public string Hashtag { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
    }
}
