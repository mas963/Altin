namespace Altin.Web.Areas.Admin.Models;

public class CreateNewsViewModel
{
    public string Title { get; set; }
    public string Content { get; set; }
    public IFormFile Image { get; set; }
}