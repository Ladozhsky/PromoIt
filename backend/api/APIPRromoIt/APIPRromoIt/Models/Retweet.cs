using System;
using System.Collections.Generic;

namespace APIPRromoIt.Models
{
    public partial class Retweet
    {
        public int RetweetId { get; set; }
        public string TwittId { get; set; } = null!;
        public string TwitterUserId { get; set; } = null!;
        public int CampaignId { get; set; }
        public int Retweets { get; set; }
        public DateTime ParsingDate { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string CreateByUser { get; set; } = null!;
        public string UpdateByUser { get; set; } = null!;
        public string Status { get; set; } = null!;
    }
}
