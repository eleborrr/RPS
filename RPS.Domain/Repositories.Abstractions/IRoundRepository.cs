using RPS.Domain.Entities;

namespace RPS.Domain.Repositories.Abstractions;

public interface IRoundRepository
{
    public Task<IEnumerable<Round>> GetAllAsync(CancellationToken cancellationToken);
    public Task<string> AddAsync(Round round);
    public Task<Round?> GetByRoundIdAsync(string matchId);
    public Task UpdateAsync(Round round);
}