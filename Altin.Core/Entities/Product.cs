namespace Altin.Core.Entities;

public class Product : BaseEntity, IAuditedEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public bool IsPopular { get; set; } = false;
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    
    public ICollection<CategoryProduct> CategoryProducts { get; set; }
}