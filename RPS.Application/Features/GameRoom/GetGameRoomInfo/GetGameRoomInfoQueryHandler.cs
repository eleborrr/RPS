using Microsoft.AspNetCore.Identity;
using RPS.Application.Dto.MediatR;
using RPS.Application.Services.Abstractions.Cqrs.Queries;
using RPS.Domain.Entities;
using RPS.Domain.Repositories.Abstractions;

namespace RPS.Application.Features.GameRoom.GetGameRoomInfo;

public class GetGameRoomInfoQueryHandler: IQueryHandler<GetGameRoomInfoQuery, GameRoomInfoDto>
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly UserManager<IdentityUser> _userManager;

    public GetGameRoomInfoQueryHandler(IRepositoryManager repositoryManager, UserManager<IdentityUser> userManager)
    {
        _repositoryManager = repositoryManager;
        _userManager = userManager;
    }

    public async Task<Result<GameRoomInfoDto>> Handle(GetGameRoomInfoQuery request, CancellationToken cancellationToken)
    {
        var gameRoom = await _repositoryManager.GameRoomRepository.GetByGameRoomIdAsync(request.GameRoomId);
        
        if (gameRoom is null)
            return new Result<GameRoomInfoDto>(null, false, $"Game room with id {request.GameRoomId} not found!");

        var creator = _userManager.Users.FirstOrDefault(u => u.Id == gameRoom.CreatorId)!;


        return new Result<GameRoomInfoDto>(
            new GameRoomInfoDto(creator.UserName, gameRoom.CreationDate, gameRoom.Id),
            true);
    }
}