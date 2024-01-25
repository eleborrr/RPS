using RPS.Application.Services.Abstractions.Cqrs.Commands;

namespace RPS.Application.Features.GameRoom.RemoveParticipant;

public record RemoveParticipantCommand(string GameRoomId, string ParticipantId): ICommand;