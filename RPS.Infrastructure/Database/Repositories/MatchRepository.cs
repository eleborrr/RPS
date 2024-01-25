using Microsoft.EntityFrameworkCore;
using RPS.Domain.Entities;
using RPS.Domain.Repositories.Abstractions;

namespace RPS.Infrastructure.Database.Repositories;

public class MatchRepository: IMatchRepository
{
    private readonly ApplicationDbContext _dbContext;

    public MatchRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<IEnumerable<Match>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<string> AddAsync(Match match)
    {
        throw new NotImplementedException();
    }

    public Task<Match?> GetByMatchIdAsync(string matchId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Match match)
    {
        throw new NotImplementedException();
    }
}