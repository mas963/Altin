using Altin.Application.Models.Product;
using Altin.Application.Services;
using Altin.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace Altin.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IProductService _productService;

    public ProductController(IWebHostEnvironment webHostEnvironment, IProductService productService)
    {
        _webHostEnvironment = webHostEnvironment;
        _productService = productService;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetAllAsync();
        
        return View(products);
    }
    
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UploadAjax([FromForm] ProductUploadViewModel model)
    {
        if (model.ProductImage == null)
        {
            return Json(new { success = false, message = "Lütfen bir dosya seçin." });
        }

        var permittedExtensions = new[] { ".jpg", ".jpeg", ".png" };
        var extension = Path.GetExtension(model.ProductImage.FileName).ToLowerInvariant();

        if (!permittedExtensions.Contains(extension))
        {
            return Json(new { success = false, message = "Sadece .jpg, .jpeg, .png uzantılı dosyalar yüklenebilir." });
        }

        var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img/Products");
        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProductName + extension;
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await model.ProductImage.CopyToAsync(fileStream);
        }

        var product = new ProductUploadModel
        {
            ProductName = model.ProductName,
            ProductDescription = model.ProductDescription,
            ProductImageName = uniqueFileName
        };

        await _productService.AddAsync(product);

        Thread.Sleep(2000); // TODO: delete

        return Json(new { success = true, message = "Ürün başarıyla eklendi" });
    }
}