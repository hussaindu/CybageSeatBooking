using CybageSeatBooking.Models;
using CybageSeatBooking.Service.Interface;
using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        if (User.Identity != null && User.Identity.IsAuthenticated)
            return RedirectToAction("Index", "Home");

        if (!ModelState.IsValid)
            return View(loginDto);

        bool success = await _accountService.LoginAsync(loginDto);

        if (success)
            return RedirectToAction("Index", "Home");

        ViewBag.ErrorMessage = "Invalid login attempt";
        return View(loginDto);
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        if (User.Identity != null && User.Identity.IsAuthenticated)
            return RedirectToAction("Index", "Home");

        if (!ModelState.IsValid)
            return View(registerDto);

        var (success, errors) = await _accountService.RegisterAsync(registerDto);

        if (success)
            return RedirectToAction("Index", "Home");

        foreach (var error in errors)
            ModelState.AddModelError("", error);

        return View(registerDto);
    }

    public async Task<IActionResult> Logout()
    {
        await _accountService.LogoutAsync();
        return RedirectToAction("Index", "Home");
    }
}
