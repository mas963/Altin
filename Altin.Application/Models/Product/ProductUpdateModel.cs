namespace Altin.Application.Models.Product;

public class ProductUpdateModel
{
    public Guid Id { get; set; }
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public string ProductImageUrl { get; set; }
    public string? HepsiburadaUrl { get; set; }
    public string? TrendyolUrl { get; set; }
    public decimal? Price { get; set; }
}