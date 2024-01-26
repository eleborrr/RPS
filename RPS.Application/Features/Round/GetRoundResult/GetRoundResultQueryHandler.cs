using RPS.Application.Dto.MediatR;
using RPS.Application.Features.Round.GetRoundResult;
using RPS.Application.Services.Abstractions;
using RPS.Application.Services.Abstractions.Cqrs.Queries;
using RPS.Domain.Repositories.Abstractions;

namespace RPS.Application.Features.Match.GetMatchResult;

public class GetRoundResultQueryHandler: IQueryHandler<GetRoundResultQuery, RoundResultDto>
{
    private readonly IRepositoryManager _repositoryManager;
    private const string RockId = "1";
    private const string PaperId = "2";
    private const string ScissorsId = "3";


    public GetRoundResultQueryHandler(IRepositoryManager repositoryManager, IServiceManager serviceManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<Result<RoundResultDto>> Handle(GetRoundResultQuery request, CancellationToken cancellationToken)
    {
        var match = await _repositoryManager.MatchRepository.GetByMatchIdAsync(request.MatchId);
        
        if (match is null)
            return new Result<RoundResultDto>(null, false, $"Match with id {request.MatchId} not exists");

        if (match.FirstUserMoveId == match.SecondUserMoveId)
            return new Result<RoundResultDto>(new RoundResultDto("-1"), true);
        
        switch (match)
        {
            case {FirstUserMoveId: ScissorsId, SecondUserMoveId: PaperId}: 
                return new Result<RoundResultDto>(new RoundResultDto(match.FirstUserId), true);
            case {FirstUserMoveId: PaperId, SecondUserMoveId: ScissorsId}: 
                return new Result<RoundResultDto>(new RoundResultDto(match.SecondUserId), true);
            
            case {FirstUserMoveId: RockId, SecondUserMoveId: ScissorsId}: 
                return new Result<RoundResultDto>(new RoundResultDto(match.FirstUserId), true);
            case {FirstUserMoveId: ScissorsId, SecondUserMoveId: RockId}: 
                return new Result<RoundResultDto>(new RoundResultDto(match.SecondUserId), true);
            
            case {FirstUserMoveId: PaperId, SecondUserMoveId: RockId}: 
                return new Result<RoundResultDto>(new RoundResultDto(match.FirstUserId), true);
            case {FirstUserMoveId: RockId, SecondUserMoveId: PaperId}: 
                return new Result<RoundResultDto>(new RoundResultDto(match.SecondUserId), true);
        }

        return new Result<RoundResultDto>(null, false, "Something went wrong. Try again later.");
    }
}