using APIPRromoIt.Models;

namespace APIPRromoIt.ModelsDto
{
    public partial class BalanceTransactionDto
    {
        public int TransactionId { get; set; }
        public string UserId { get; set; } = null!;
        public int CampaignId { get; set; }
        public int Amount { get; set; }
        public string? Reason { get; set; }
        public int? RetweetId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string CreateByUser { get; set; } = null!;
        public string UpdateByUser { get; set; } = null!;

    }
}
