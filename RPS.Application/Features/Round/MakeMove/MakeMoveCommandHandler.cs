using BeaverTinder.Application.Services.Abstractions.Cqrs.Commands;
using RPS.Application.Dto.MediatR;
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
        var match = await _repositoryManager.MatchRepository.GetByMatchIdAsync(request.MatchId);

        if (match is null)
            return new Result(false, $"Match with id {request.MatchId} not exists");

        if (request.UserId == match.FirstUserId)
            match.FirstUserMoveId = request.MoveId;
        else if (request.UserId == match.SecondUserMoveId)
            match.SecondUserMoveId = request.MoveId;
        else
            return new Result(false, "You're not a participant in that game");

        await _repositoryManager.MatchRepository.UpdateAsync(match);
        //UIO saveasync??
        return new Result(true);
    }
}