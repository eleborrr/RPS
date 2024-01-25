using RPS.Application.Services.Abstractions.Cqrs.Commands;

namespace RPS.Application.Features.GameRoom.AddParticipant;

public record AddParticipantCommand(string GameRoomId, string ParticipantId): ICommand;