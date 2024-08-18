using Altin.Shared.Helpers;

namespace Altin.Core.Entities;

public class News : BaseEntity, IAuditedEntity
{
    private string title;

    public string Title
    {
        get => title;
        set
        {
            title = value;
            NormalizedTitle = StringHelper.NormalizeString(value);
        }
    }

    public string NormalizedTitle { get; private set; }
    public string Content { get; set; }
    public string? ImageName { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
}