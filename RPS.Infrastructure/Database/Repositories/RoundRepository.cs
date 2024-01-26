using Microsoft.EntityFrameworkCore;
using RPS.Domain.Entities;
using RPS.Domain.Repositories.Abstractions;

namespace RPS.Infrastructure.Database.Repositories;

public class RoundRepository: IRoundRepository
{
    private readonly ApplicationDbContext _dbContext;

    public RoundRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Round>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Rounds.ToListAsync(cancellationToken);
    }

    public async Task<string> AddAsync(Round round)
    {
        var id = await _dbContext.Rounds.AddAsync(round);
        await _dbContext.SaveChangesAsync();
        return id.Entity.Id;
    }

    public async Task<Round?> GetByRoundIdAsync(string roundId)
    {
        return await _dbContext.Rounds.FirstOrDefaultAsync(x => x.Id == roundId);
    }

    public async Task UpdateAsync(Round round)
    {
        var roundInDb = _dbContext.Rounds.FirstOrDefault(r => r.Id == round.Id);
        if (roundInDb is null)
            return;
        _dbContext.Entry(roundInDb).CurrentValues.SetValues(round);
        await _dbContext.SaveChangesAsync();
    }
}