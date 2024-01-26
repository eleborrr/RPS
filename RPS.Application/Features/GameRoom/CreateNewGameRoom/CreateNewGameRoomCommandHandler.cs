using BeaverTinder.Application.Services.Abstractions.Cqrs.Commands;
using Microsoft.AspNetCore.Identity;
using RPS.Application.Dto.MediatR;
using RPS.Domain.Repositories.Abstractions;

namespace RPS.Application.Features.GameRoom.CreateNewGameRoom;

public class CreateNewGameRoomCommandHandler: ICommandHandler<CreateNewGameRoomCommand, string>
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly UserManager<IdentityUser> _userManager;

    public CreateNewGameRoomCommandHandler(IRepositoryManager repositoryManager, UserManager<IdentityUser> userManager)
    {
        _repositoryManager = repositoryManager;
        _userManager = userManager;
    }

    public async Task<Result<string>> Handle(CreateNewGameRoomCommand request, CancellationToken cancellationToken)
    {
        var creator = _userManager.Users.FirstOrDefault(u => u.Id == request.CreatorId);
        var res= await _repositoryManager.GameRoomRepository.AddAsync(new Domain.Entities.GameRoom
        {
            Id = Guid.NewGuid().ToString(),
            ParticipantId = "-1",
            TimeToMove = request.TimeToMove,
            EloDelta = request.EloDelta,
            CreatorId = request.CreatorId,
            CreatorConnected = true
        });
        return new Result<string>(res, true);
    }
}