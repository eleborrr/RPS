using RPS.Domain.Entities;

namespace RPS.Domain.Repositories.Abstractions;

public interface IMatchRepository
{
    public Task<IEnumerable<Match>> GetAllAsync(CancellationToken cancellationToken);
    public Task<string> AddAsync(Match match);
    public Task<Match?> GetByMatchIdAsync(string matchId);
    public Task UpdateAsync(Match match);
}