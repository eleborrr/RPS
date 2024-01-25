using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RPS.Application.Dto.Authentication.Login;
using RPS.Application.Features.GameRoom.GetGameRoomInfo;
using RPS.Application.Services.Abstractions;
using RPS.Domain.Entities;

namespace RPS.API.Controllers;

[ApiController]
[Route("[controller]")]
public class GameRoomController : Controller
{
    private readonly IServiceManager _serviceManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IMediator _mediator;

    public GameRoomController(
        IServiceManager serviceManager,
        SignInManager<User> signInManager,
        IConfiguration configuration,
        HttpClient client,
        UserManager<User> userManager, IMediator mediator)
    {
        _serviceManager = serviceManager;
        _signInManager = signInManager;
        _mediator = mediator;
    }

    [HttpGet("/gameroom_info")]
    public async Task<JsonResult> GetGameRoomInfo([FromQuery] string id)
    {
        var gameRoomInfo = await _mediator.Send(new GetGameRoomInfoQuery(id));
        if (gameRoomInfo.IsSuccess)
            return Json(gameRoomInfo.Value);
        
        //TODO throw 404
        return Json(gameRoomInfo.Error);
    }
}