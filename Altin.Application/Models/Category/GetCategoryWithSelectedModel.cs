namespace Altin.Application.Models.Category;

public class GetCategoryWithSelectedModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int CategoryOrder { get; set; }
    public bool Selected { get; set; }
}