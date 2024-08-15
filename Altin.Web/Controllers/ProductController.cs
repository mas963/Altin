using Microsoft.AspNetCore.Mvc;

namespace Altin.Web.Controllers;

[Route("Urunler")]
public class ProductController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}