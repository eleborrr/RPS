﻿using BeaverTinder.Application.Services.Abstractions.Cqrs.Commands;
using RPS.Application.Dto.MediatR;
using RPS.Domain.Repositories.Abstractions;

namespace RPS.Application.Features.Match.CreateNewMatch;

public class CreateNewMatchCommandHandler: ICommandHandler<CreateNewMatchCommand, string>
{
    private readonly IRepositoryManager _repositoryManager;

    public CreateNewMatchCommandHandler(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<Result<string>> Handle(CreateNewMatchCommand request, CancellationToken cancellationToken)
    {
        var id = await _repositoryManager.MatchRepository.AddAsync(new Domain.Entities.Match
        {
            Id = Guid.NewGuid().ToString(),
            FirstUserId = request.FirstUserId,
            SecondUserId = request.SecondUserId
        });
        return new Result<string>(id, true);
    }
}