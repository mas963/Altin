namespace Altin.Web.Areas.Admin.Models;

public class ProductUploadViewModel
{
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public IFormFile ProductImage { get; set; }
    public string Location { get; set; }
}