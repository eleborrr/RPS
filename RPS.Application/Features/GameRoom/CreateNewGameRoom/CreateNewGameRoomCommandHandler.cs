using BeaverTinder.Application.Services.Abstractions.Cqrs.Commands;
using RPS.Application.Dto.MediatR;
using RPS.Domain.Repositories.Abstractions;

namespace RPS.Application.Features.GameRoom.CreateNewGameRoom;

public class CreateNewGameRoomCommandHandler: ICommandHandler<CreateNewGameRoomCommand, string>
{
    private readonly IRepositoryManager _repositoryManager;

    public CreateNewGameRoomCommandHandler(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<Result<string>> Handle(CreateNewGameRoomCommand request, CancellationToken cancellationToken)
    {
        var res= await _repositoryManager.GameRoomRepository.AddAsync(new Domain.Entities.GameRoom
        {
            TimeToMove = request.TimeToMove,
            EloDelta = request.EloDelta,
            CreatorId = request.CreatorId
        });
        return new Result<string>(res, true);
    }
}