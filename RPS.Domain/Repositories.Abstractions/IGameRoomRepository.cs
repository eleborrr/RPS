using RPS.Domain.Entities;

namespace RPS.Domain.Repositories.Abstractions;

public interface IGameRoomRepository
{
    public Task<IEnumerable<GameRoom>> GetAllAsync(CancellationToken cancellationToken);
    public Task<string> AddAsync(GameRoom match);
    public Task<GameRoom?> GetByGameRoomIdAsync(string gameRoomId);
    public Task UpdateAsync(GameRoom match);
}