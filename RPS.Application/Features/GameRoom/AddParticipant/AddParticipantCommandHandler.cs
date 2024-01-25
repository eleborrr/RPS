using BeaverTinder.Application.Services.Abstractions.Cqrs.Commands;
using RPS.Application.Dto.MediatR;
using RPS.Domain.Repositories.Abstractions;

namespace RPS.Application.Features.GameRoom.AddParticipant;

public class AddParticipantCommandHandler: ICommandHandler<AddParticipantCommand>
{
    private readonly IRepositoryManager _repositoryManager;

    public AddParticipantCommandHandler(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<Result> Handle(AddParticipantCommand request, CancellationToken cancellationToken)
    {
        var gameRoom = await _repositoryManager.GameRoomRepository.GetByGameRoomIdAsync(request.GameRoomId);

        if (gameRoom is null)
            return new Result(false, $"Room with id {request.GameRoomId} not found!");
        
        if (gameRoom.Participant is null)
            gameRoom.Participant = request.ParticipantId;
        else
            return new Result(false, "Cannot join this game. Lobby is full!");
        await _repositoryManager.GameRoomRepository.UpdateAsync(gameRoom);
        return new Result(true);
    }
}