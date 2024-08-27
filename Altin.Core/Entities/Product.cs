using Altin.Shared.Helpers;

namespace Altin.Core.Entities;

public class Product : BaseEntity, IAuditedEntity
{
    private string name;

    public string Name
    {
        get => name;
        set
        {
            name = value;
            Slug = StringHelper.NormalizeString(value);
        }
    }
    public string Slug { get; private set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public decimal? Price { get; set; }
    public string? HepsiburadaUrl { get; set; }
    public string? TrendyolUrl { get; set; }
    public bool IsPopular { get; set; } = false;
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    
    
    
    public ICollection<CategoryProduct> CategoryProducts { get; set; }
}