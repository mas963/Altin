namespace Altin.Web.Areas.Admin.Models;

public class ProductUploadViewModel
{
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public IFormFile ProductImage { get; set; }
    public decimal? Price { get; set; }
    public string? HepsiburadaUrl { get; set; }
    public string? TrendyolUrl { get; set; }
    public bool IsPopularProduct { get; set; }
    public List<Guid>? CategoryIds { get; set; }
}