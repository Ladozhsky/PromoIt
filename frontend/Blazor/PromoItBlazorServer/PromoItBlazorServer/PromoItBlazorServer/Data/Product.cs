namespace PromoItBlazorServer.Data
{
    public partial class ProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int Price { get; set; }
        public string? Image { get; set; }
        public string CompanyName { get; set; }
    }
}
