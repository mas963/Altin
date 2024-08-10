namespace Altin.Application.Models.Product;

public class ProductUpdateModel
{
    public Guid Id { get; set; }
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public string ProductImageUrl { get; set; }
}