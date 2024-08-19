namespace Altin.Application.Models.News;

public class GetNewsModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public string Content { get; set; }
    public string? ImageName { get; set; }
    public DateTime CreatedOn { get; set; }
}