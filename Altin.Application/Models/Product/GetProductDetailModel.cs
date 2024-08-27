namespace Altin.Application.Models.Product;

public class GetProductDetailModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public decimal? Price { get; set; }
    public string? HepsiBuradaUrl { get; set; }
    public string? TrendyolUrl { get; set; }
    public bool IsPopular { get; set; }
    
    public List<string> Categories { get; set; }
}