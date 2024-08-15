namespace Altin.Application.Models.Product;

public class ProductUploadModel
{
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public string ProductImageName { get; set; }
    public bool IsPopularProduct { get; set; }
    public List<Guid> CategoryIds { get; set; }
}