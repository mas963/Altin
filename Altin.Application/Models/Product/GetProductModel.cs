namespace Altin.Application.Models.Product;

public class GetProductModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public bool IsPopular { get; set; }
}