using Microsoft.AspNetCore.Mvc;

namespace Altin.Controllers;

[Route("Hakkimizda")]
public class AboutController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}