using Altin.Shared.Helpers;

namespace Altin.Core.Entities;

public class Category : BaseEntity
{
    public string Name
    {
        get => name;
        set
        {
            name = value;
            NormalizeName = StringHelper.NormalizeString(value);
        }
    }
    
    public string NormalizeName { get; private set; }

    private string name;
    
    public int CategoryOrder { get; set; }

    public ICollection<CategoryProduct> CategoryProducts { get; set; }
}