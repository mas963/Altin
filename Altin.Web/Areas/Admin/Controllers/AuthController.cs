using Altin.Application.Models.User;
using Altin.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Altin.Areas.Admin.Controllers;

[Area("Admin")]
public class AuthController : Controller
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    // GET
    public async Task<IActionResult> Login()
    {
        // var createUserModel = new CreateUserModel
        // {
        //     Username = "admin",
        //     Email = "admin@gmail.com",
        //     Password = "Admin123!",
        // };
        //
        // await _userService.CreateAsync(createUserModel);
        
        if (User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginUserModel model, string returnUrl = null)
    {
        if (ModelState.IsValid)
        {
            var result = await _userService.LoginAsync(model);

            if (result.Succeeded)
            {
                return RedirectToLocal(returnUrl);
            }

            if (result.IsLockedOut)
            {
                // Lockout handling
                return RedirectToAction("Lockout");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Geçersiz giriş denemesi.");
                return View(model);
            }
        }

        // Eğer bu noktaya gelinirse, form yeniden görüntülenir.
        return View(model);
    }

    private IActionResult RedirectToLocal(string returnUrl)
    {
        if (Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl);
        }
        else
        {
            return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
        }
    }

    public async Task<IActionResult> Logout()
    {
        await _userService.LogoutAsync();

        return RedirectToAction("Login", "Auth", new { area = "Admin" });

    }
}