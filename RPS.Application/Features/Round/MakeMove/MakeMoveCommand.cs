using RPS.Application.Services.Abstractions.Cqrs.Commands;

namespace RPS.Application.Features.Match.MakeMove;

public record MakeMoveCommand(string MatchId, string MoveId, string UserId): ICommand;