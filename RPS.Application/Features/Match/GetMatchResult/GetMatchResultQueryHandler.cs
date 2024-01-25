using RPS.Application.Dto.MediatR;
using RPS.Application.Features.Game.GetGameResult;
using RPS.Application.Services.Abstractions;
using RPS.Application.Services.Abstractions.Cqrs.Queries;
using RPS.Domain.Repositories.Abstractions;

namespace RPS.Application.Features.Match.GetMatchResult;

public class GetMatchResultQueryHandler: IQueryHandler<GetMatchResultQuery, MatchResultDto>
{
    private readonly IRepositoryManager _repositoryManager;
    private const string RockId = "1";
    private const string PaperId = "2";
    private const string ScissorsId = "3";


    public GetMatchResultQueryHandler(IRepositoryManager repositoryManager, IServiceManager serviceManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<Result<MatchResultDto>> Handle(GetMatchResultQuery request, CancellationToken cancellationToken)
    {
        var match = await _repositoryManager.MatchRepository.GetByMatchIdAsync(request.MatchId);
        
        if (match is null)
            return new Result<MatchResultDto>(null, false, $"Match with id {request.MatchId} not exists");

        if (match.FirstUserMoveId == match.SecondUserMoveId)
            return new Result<MatchResultDto>(null, true);
        
        //TODO захардкоженная проверка на id. Че с ними делать?
        switch (match)
        {
            case {FirstUserMoveId: ScissorsId, SecondUserMoveId: PaperId}: 
                return new Result<MatchResultDto>(new MatchResultDto(match.FirstUserId), true);
            case {FirstUserMoveId: PaperId, SecondUserMoveId: ScissorsId}: 
                return new Result<MatchResultDto>(new MatchResultDto(match.SecondUserId), true);
            
            case {FirstUserMoveId: RockId, SecondUserMoveId: ScissorsId}: 
                return new Result<MatchResultDto>(new MatchResultDto(match.FirstUserId), true);
            case {FirstUserMoveId: ScissorsId, SecondUserMoveId: RockId}: 
                return new Result<MatchResultDto>(new MatchResultDto(match.SecondUserId), true);
            
            case {FirstUserMoveId: PaperId, SecondUserMoveId: RockId}: 
                return new Result<MatchResultDto>(new MatchResultDto(match.FirstUserId), true);
            case {FirstUserMoveId: RockId, SecondUserMoveId: PaperId}: 
                return new Result<MatchResultDto>(new MatchResultDto(match.SecondUserId), true);
        }

        return new Result<MatchResultDto>(null, false, "Something went wrong. Try again later.");
    }
}