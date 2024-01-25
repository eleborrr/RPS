using Microsoft.EntityFrameworkCore;
using RPS.Domain.Entities;
using RPS.Domain.Repositories.Abstractions;

namespace RPS.Infrastructure.Database.Repositories;

public class GameRoomRepository: IGameRoomRepository
{
    private readonly ApplicationDbContext _dbContext;

    public GameRoomRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<GameRoom>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.GameRooms.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(GameRoom room)
    {
        await _dbContext.GameRooms.AddAsync(room);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<GameRoom?> GetByGameRoomIdAsync(string gameRoomId)
    {
        return await _dbContext.GameRooms.FirstOrDefaultAsync(x => x.Id == gameRoomId);
    }
}