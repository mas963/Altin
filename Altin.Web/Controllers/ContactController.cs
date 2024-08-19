using Microsoft.AspNetCore.Mvc;

namespace Altin.Web.Controllers;
[Route("İletişim")]

public class ContactController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}

