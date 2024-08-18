using Altin.Application.Exceptions;
using Altin.Application.Models.Product;
using Altin.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Altin.Web.Controllers;

[Route("Urunler")]
public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public ProductController(ICategoryService categoryService, IProductService productService)
    {
        _categoryService = categoryService;
        _productService = productService;
    }

    public async Task<IActionResult> Index(string kategori, int sayfa = 1)
    {
        GetProductWithPagination result;

        try
        {
            if (kategori != null)
            {
                result = await _productService.GetAllByCategoryAsync(kategori, page: sayfa);
            }
            else
            {
                result = await _productService.GetAllAsync(page: sayfa);
            }
        }
        catch (NotFoundException)
        {
            return RedirectToAction("Index", "Home");
        }
        
        ViewData["Categories"] = await _categoryService.GetAllAsync();

        return View(result);
    }
}