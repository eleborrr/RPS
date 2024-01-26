using RPS.Application.Services.Abstractions.Cqrs.Queries;

namespace RPS.Application.Features.GameRoom.GetGameRoomInfo;

public record GetGameRoomInfoQuery(string GameRoomId): IQuery<GameRoomInfoDto>;