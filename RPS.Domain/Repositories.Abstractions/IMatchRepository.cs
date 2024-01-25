using RPS.Domain.Entities;

namespace RPS.Domain.Repositories.Abstractions;

public interface IMatchRepository
{
    public Task<IEnumerable<Match>> GetAllAsync(CancellationToken cancellationToken);
    public Task AddAsync(Match match);
    public Task<Match?> GetByMatchIdAsync(string matchId);
}