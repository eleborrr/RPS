﻿namespace RPS.Application.Features.Round.GetRoundResult;

public record RoundResultDto(string WinnerId, string LoserId, bool IsDraw);