using APIPRromoIt.Models;

namespace APIPRromoIt.ModelsDto
{
    public partial class BalanceTransactionDto
    {
        public int CampaignId { get; set; }
        public int Amount { get; set; }
    }

    public partial class DollarsByUser
    {
        public string CampaignName { get; set; }
        public int Dollars { get; set; }
    }
}
