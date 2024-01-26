using RPS.Application.Services.Abstractions.Cqrs.Queries;

namespace RPS.Application.Features.Round.GetRoundResult;

public record GetRoundResultQuery(string MatchId): IQuery<RoundResultDto>;