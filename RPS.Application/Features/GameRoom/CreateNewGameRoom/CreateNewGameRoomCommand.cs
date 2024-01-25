using RPS.Application.Services.Abstractions.Cqrs.Commands;

namespace RPS.Application.Features.GameRoom.CreateNewGameRoom;

public record CreateNewGameRoomCommand(int TimeToMove, int EloDelta, string CreatorId): ICommand<string>;