using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RPS.Application.Dto.Authentication.Login;
using RPS.Application.Features.GameRoom.GetAllRooms;
using RPS.Application.Features.GameRoom.GetGameRoomInfo;
using RPS.Application.Services.Abstractions;
using RPS.Domain.Entities;

namespace RPS.API.Controllers;

[ApiController]
[Route("[controller]")]
public class GameRoomController : Controller
{
    private readonly IServiceManager _serviceManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IMediator _mediator;

    public GameRoomController(
        IServiceManager serviceManager,
        SignInManager<IdentityUser> signInManager,
        IConfiguration configuration,
        HttpClient client,
        UserManager<IdentityUser> userManager, IMediator mediator)
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

    [HttpGet("/allRooms")]
    public async Task<JsonResult> GetAllGameRooms()
    {
        var allRooms = await _mediator.Send(new GetAllRoomsQuery());
        if (allRooms.IsSuccess)
            return Json(allRooms.Value);
        
        //TODO throw 404
        return Json(allRooms.Error);
    }
}