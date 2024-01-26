using BeaverTinder.Application.Services.Abstractions.Cqrs.Commands;
using RPS.Application.Dto.MediatR;
using RPS.Domain.Repositories.Abstractions;

namespace RPS.Application.Features.Match.CreateNewMatch;

public class CreateNewRoundCommandHandler: ICommandHandler<CreateNewRoundCommand, string>
{
    private readonly IRepositoryManager _repositoryManager;

    public CreateNewRoundCommandHandler(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<Result<string>> Handle(CreateNewRoundCommand request, CancellationToken cancellationToken)
    {
        var id = await _repositoryManager.RoundRepository.AddAsync(new Domain.Entities.Round
        {
            Id = Guid.NewGuid().ToString(),
            FirstUserId = request.FirstUserId,
            SecondUserId = request.SecondUserId
        });
        return new Result<string>(id, true);
    }
}