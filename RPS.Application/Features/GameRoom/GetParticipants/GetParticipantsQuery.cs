using RPS.Application.Services.Abstractions.Cqrs.Queries;

namespace RPS.Application.Features.GameRoom.GetParticipants;

public record GetParticipantsQuery(string GameRoomId): IQuery<IEnumerable<ParticipantDto>>;