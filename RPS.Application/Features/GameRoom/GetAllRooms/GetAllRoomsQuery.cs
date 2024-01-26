using RPS.Application.Services.Abstractions.Cqrs.Queries;

namespace RPS.Application.Features.GameRoom.GetAllRooms;

public record GetAllRoomsQuery(): IQuery<List<GetAllRoomsDto>>;