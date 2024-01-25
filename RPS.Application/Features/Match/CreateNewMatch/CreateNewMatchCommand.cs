using RPS.Application.Services.Abstractions.Cqrs.Commands;

namespace RPS.Application.Features.Match.CreateNewMatch;

public record CreateNewMatchCommand(string FirstUserId, string SecondUserId): ICommand<string>;