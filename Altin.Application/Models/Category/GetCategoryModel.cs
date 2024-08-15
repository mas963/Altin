namespace Altin.Application.Models.Category;

public class GetCategoryModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int CategoryOrder { get; set; }
}