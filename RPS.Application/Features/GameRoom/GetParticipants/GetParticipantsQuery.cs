using RPS.Application.Services.Abstractions.Cqrs.Queries;

namespace RPS.Application.Features.GameRoom.GetParticipants;

public class GetParticipantsQuery: IQuery<IEnumerable<ParticipantDto>>
{
    
}