using RPS.Application.Dto.MediatR;
using RPS.Application.Services.Abstractions.Cqrs.Commands;
using RPS.Domain.Repositories.Abstractions;

namespace RPS.Application.Features.Match.MakeMove;

public class MakeMoveCommandHandler: ICommandHandler<MakeMoveCommand>
{
    private readonly IRepositoryManager _repositoryManager;

    public MakeMoveCommandHandler(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<Result> Handle(MakeMoveCommand request, CancellationToken cancellationToken)
    {
        var round = await _repositoryManager.RoundRepository.GetByRoundIdAsync(request.MatchId);

        if (round is null)
            return new Result(false, $"Match with id {request.MatchId} not exists");

        if (request.UserId == round.FirstUserId)
            round.FirstUserMoveId = request.MoveId;
        else if (request.UserId == round.SecondUserMoveId)
            round.SecondUserMoveId = request.MoveId;
        else
            return new Result(false, "You're not a participant in that game");

        await _repositoryManager.RoundRepository.UpdateAsync(round);
        //UIO saveasync??
        return new Result(true);
    }
}