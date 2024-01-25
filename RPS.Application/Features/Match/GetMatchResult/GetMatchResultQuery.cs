using RPS.Application.Features.Game.GetGameResult;
using RPS.Application.Services.Abstractions.Cqrs.Queries;

namespace RPS.Application.Features.Match.GetMatchResult;

public record GetMatchResultQuery(string MatchId): IQuery<MatchResultDto>;