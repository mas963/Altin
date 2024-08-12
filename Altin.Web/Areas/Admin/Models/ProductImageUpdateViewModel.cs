using System;
using Microsoft.AspNetCore.Http;

namespace Altin.Web.Areas.Admin.Models;

public class ProductImageUpdateViewModel
{
    public Guid ProductId { get; set; }
    public IFormFile NewProductImage { get; set; }
}