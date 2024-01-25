using System.Globalization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using RPS.Application.Dto.Authentication.Login;
using RPS.Application.Services.Abstractions;
using RPS.Domain.Entities;
using RPS.Domain.Services.Abstractions;

namespace RPS.API.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : Controller
{
    private readonly IServiceManager _serviceManager;
    private readonly SignInManager<User> _signInManager;

    public LoginController(
        IServiceManager serviceManager,
        SignInManager<User> signInManager,
        IConfiguration configuration,
        HttpClient client,
        UserManager<User> userManager)
    {
        _serviceManager = serviceManager;
        _signInManager = signInManager;
    }

    [HttpPost]
    public async Task<JsonResult> Login([FromBody] LoginRequestDto model)
    {
        return Json(await _serviceManager.AccountService.Login(model, ModelState));
    }

    [HttpGet("/logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Login");
    }
}
