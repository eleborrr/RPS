using Microsoft.AspNetCore.Identity;
using RPS.Application.Dto.MediatR;
using RPS.Application.Services.Abstractions.Cqrs.Queries;
using RPS.Domain.Repositories.Abstractions;

namespace RPS.Application.Features.GameRoom.GetAllRooms;

public class GetAllRoomsQueryHandler : IQueryHandler<GetAllRoomsQuery, List<GetAllRoomsDto>>
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly UserManager<IdentityUser> _userManager;

    public GetAllRoomsQueryHandler(IRepositoryManager repositoryManager, UserManager<IdentityUser> userManager)
    {
        _repositoryManager = repositoryManager;
        _userManager = userManager;
    }

    public async Task<Result<List<GetAllRoomsDto>>> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
    {
        var rooms = await _repositoryManager.GameRoomRepository.GetAllAsync(cancellationToken);
        if (rooms is null)
        {
            return new Result<List<GetAllRoomsDto>>(null, false, "there are no rooms at the moment");
        }

        var result = new List<GetAllRoomsDto>();
        foreach (var room in rooms)
        {
            var users = _userManager.Users.ToList();
            var creator = await _userManager.FindByIdAsync(room.CreatorId);
            if (creator != null)
                result.Add(new GetAllRoomsDto(creator.UserName, room.CreationDate, room.Id));
        }
        
        // var roomTasks = rooms
        //     .Where(r => r.CreatorId != "")
        //     .Select(async r =>
        // {
        //     var creator = await _userManager.FindByIdAsync(r.CreatorId);
        //     return new GetAllRoomsDto(creator.UserName, r.CreationDate, r.Id);
        // });
        //
        // var dtos = await Task.WhenAll(roomTasks);

        return new Result<List<GetAllRoomsDto>>(result, true);
    }
}