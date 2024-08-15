using Microsoft.AspNetCore.Mvc;

namespace Altin.Web.Controllers;

[Route("Haberler")]
public class NewsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}