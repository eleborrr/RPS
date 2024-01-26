using RPS.Application.Dto.MediatR;
using RPS.Application.Services.Abstractions.Cqrs.Queries;
using RPS.Domain.Repositories.Abstractions;

namespace RPS.Application.Features.GameRoom.GetParticipants;

public class GetParticipantsQueryHandler: IQueryHandler<GetParticipantsQuery, IEnumerable<ParticipantDto>>
{
    private readonly IRepositoryManager _repositoryManager;

    public GetParticipantsQueryHandler(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<Result<IEnumerable<ParticipantDto>>> Handle(GetParticipantsQuery request, CancellationToken cancellationToken)
    {
        var gameRoomInfo = await _repositoryManager.GameRoomRepository.GetByGameRoomIdAsync(request.GameRoomId);
        throw new NotImplementedException();
    }
}