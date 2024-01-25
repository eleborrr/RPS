using Microsoft.AspNetCore.Mvc;
using RPS.Application.Dto.Authentication.Register;
using RPS.Application.Services.Abstractions;
using RPS.Domain.Services.Abstractions;

namespace RPS.API.Controllers;

[ApiController]
[Route("[controller]")]
public class RegistrationController : Controller
{
    private readonly IServiceManager _serviceManager;
    public RegistrationController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpPost]
    public async Task<JsonResult> Register([FromBody]RegisterRequestDto model)
    {
        return Json(await _serviceManager.AccountService.Register(model, ModelState));
    }
}