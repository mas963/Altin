namespace Altin.Application.Models.Product;

public class ProductUpdateReq
{
    public Guid Id { get; set; }
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public string? HepsiburadaUrl { get; set; }
    public string? TrendyolUrl { get; set; }
    public decimal? Price { get; set; }
    public List<Guid> CategoryIds { get; set; }
}