using APIPRromoIt.Models;

namespace APIPRromoIt.ModelsDto
{
    public class CompanyDto
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; } = null!;
        public string Site { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string CompanyType { get; set; }
    }
}
