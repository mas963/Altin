namespace Altin.Web.Areas.Admin.Models;

public class ProductUpdateReq
{
    public Guid Id { get; set; }
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public List<Guid> CategoryIds { get; set; }
}