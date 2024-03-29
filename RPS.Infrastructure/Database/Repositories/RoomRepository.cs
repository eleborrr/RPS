﻿using Microsoft.EntityFrameworkCore;
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

    public async Task<string> AddAsync(GameRoom room)
    {
        var id = await _dbContext.GameRooms.AddAsync(room);
        await _dbContext.SaveChangesAsync();
        return id.Entity.Id;
    }

    public async Task UpdateAsync(GameRoom room)
    {
        var gameRoomInDb = _dbContext.GameRooms.FirstOrDefault(g => g.Id == room.Id);
        if (gameRoomInDb is null)
            return;
        _dbContext.Entry(gameRoomInDb).CurrentValues.SetValues(room);
        await _dbContext.SaveChangesAsync();
        
    }

    public async Task<GameRoom?> GetByGameRoomIdAsync(string gameRoomId)
    {
        return await _dbContext.GameRooms.FirstOrDefaultAsync(x => x.Id == gameRoomId);
    }
}