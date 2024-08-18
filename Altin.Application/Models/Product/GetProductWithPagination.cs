namespace Altin.Application.Models.Product;

public class GetProductWithPagination
{
    public List<GetProductModel> Products { get; set; }
    public string? CategoryName { get; set; }
    public string? CategoryNormalizeName { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
}