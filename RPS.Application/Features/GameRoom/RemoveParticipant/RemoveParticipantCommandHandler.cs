using BeaverTinder.Application.Services.Abstractions.Cqrs.Commands;
using RPS.Application.Dto.MediatR;
using RPS.Domain.Repositories.Abstractions;

namespace RPS.Application.Features.GameRoom.RemoveParticipant;

public class RemoveParticipantCommandHandler: ICommandHandler<RemoveParticipantCommand>
{
    private readonly IRepositoryManager _repositoryManager;

    public RemoveParticipantCommandHandler(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<Result> Handle(RemoveParticipantCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
        // var gameRoom = await _repositoryManager.GameRoomRepository.GetByGameRoomIdAsync(request.GameRoomId);
        //
        // if (gameRoom is null)
        //     return new Result(false, $"Game room with id {request.GameRoomId} not found!");
        //
        // if (gameRoom.Participant1 == request.ParticipantId)
        //     gameRoom.Participant1 = null;
        // else if (gameRoom.Participant2 == request.ParticipantId)
        //     gameRoom.Participant2 = null;
        // else
        //     return new Result(false, $"Participant with {request.ParticipantId} id not found this game room!");
        // await _repositoryManager.GameRoomRepository.UpdateAsync(gameRoom);
        // return new Result(true);
    }
}