using Microsoft.AspNetCore.Http;

namespace Altin.Web.Areas.Admin.Models;

public class ProductUploadViewModel
{
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public IFormFile ProductImage { get; set; }
    public bool IsPopularProduct { get; set; }
}