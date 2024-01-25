using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RPS.Application.Dto.Account;
using RPS.Application.Dto.ResponsesAbstraction;
using RPS.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using RPS.Application.Services.Abstractions;


namespace RPS.API.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("[controller]")]
public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IServiceManager _serviceManager;
    private readonly IMediator _mediator;
    public AccountController(
        IServiceManager serviceManager,
        UserManager<User> userManager,
        IMediator mediator)
    {
        _userManager = userManager;
        _mediator = mediator;
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
    
    [HttpGet("/all")]
    public Task<JsonResult> GetAllUsers(CancellationToken cancellationToken)
    {
        var users = _userManager.Users;
        
        var result = new List<User>();
        foreach (var user in users)
        {
            result.Add(new ()
            {
                Username = user.UserName!,
                Id = user.Id,
            });
        }
        return Task.FromResult(Json(result));
    }
}