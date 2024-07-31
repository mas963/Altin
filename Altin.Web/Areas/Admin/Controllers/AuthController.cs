using Microsoft.AspNetCore.Mvc;

namespace Altin.Areas.Admin.Controllers;

[Area("Admin")]
public class AuthController : Controller
{
    // GET
    public async Task<IActionResult> Login()
    {
        return View();
    }
}