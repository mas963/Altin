using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Altin.Controllers;

[Route("Hakkimizda")]
[Authorize]
public class AboutController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}