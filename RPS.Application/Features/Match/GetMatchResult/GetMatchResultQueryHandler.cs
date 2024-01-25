using RPS.Application.Dto.MediatR;
using RPS.Application.Features.Game.GetGameResult;
using RPS.Application.Services.Abstractions;
using RPS.Application.Services.Abstractions.Cqrs.Queries;
using RPS.Domain.Repositories.Abstractions;

namespace RPS.Application.Features.Match.GetMatchResult;

public class GetMatchResultQueryHandler: IQueryHandler<GetMatchResultQuery, MatchResultDto>
{
    private readonly IRepositoryManager _repositoryManager;

    public GetMatchResultQueryHandler(IRepositoryManager repositoryManager, IServiceManager serviceManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<Result<MatchResultDto>> Handle(GetMatchResultQuery request, CancellationToken cancellationToken)
    {
        var match = await _repositoryManager.MatchRepository.GetByMatchIdAsync(request.MatchId);
        
        //TODO обработка несущсетсвующего матча
        if (match is null)
            return new Result<MatchResultDto>(null, false, $"Match with id {request.MatchId} not exists");

        if (match.FirstUserMoveId == match.SecondUserMoveId)
            return new Result<MatchResultDto>(null, true);
        
        var 
        
        if
    }
}