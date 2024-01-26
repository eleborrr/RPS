using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RPS.Application.Dto.Account;
using RPS.Application.Dto.ResponsesAbstraction;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using RPS.Application.Services.Abstractions;
using RPS.Domain.Repositories.Abstractions;
using RPS.Shared.Rating;


namespace RPS.API.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("[controller]")]
public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IServiceManager _serviceManager;
    private readonly IBus _bus;
    
    public AccountController(
        IServiceManager serviceManager,
        UserManager<IdentityUser> userManager, IBus bus)
    {
        _userManager = userManager;
        _bus = bus;
        _serviceManager = serviceManager;
    }

    [HttpGet("/userinfo")]
    public async Task<JsonResult> GetAccountInformation([FromQuery] string id, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user is null)
            return new JsonResult(new FailResponse(false, "User not found", 404));
        
        var model = new EditUserRequestDto
        {
            UserName = user.UserName!,
        };
        return Json(model);
    }

    [HttpPost("/edit")]
    public async Task<JsonResult> EditAccount([FromBody] EditUserRequestDto model)
    {
        var s = User.Claims.FirstOrDefault(c => c.Type == "Id")!;
        var user = await _userManager.FindByIdAsync(s.Value);

        if (user is null)
            return new JsonResult(new FailResponse(
                false,
                "User not found",
                404));
        
        var b = await _serviceManager.AccountService.EditAccount(user, model, ModelState);
        return Json(b);
    }

    [HttpPost("/adjust")]
    public async Task<JsonResult> AdjustRatingTest([FromQuery] string userId, [FromBody] int adjust)
    {
        await _bus.Send(new AdjustUserRatingMongoDto(userId, adjust));
        return new JsonResult(":D");
    }
    
    [HttpGet("/all")]
    public Task<JsonResult> GetAllUsers(CancellationToken cancellationToken)
    {
        var users = _userManager.Users;
        
        var result = new List<IdentityUser>();
        foreach (var user in users)
        {
            result.Add(new ()
            {
                UserName = user.UserName!,
                Id = user.Id,
            });
        }
        return Task.FromResult(Json(result));
    }
}