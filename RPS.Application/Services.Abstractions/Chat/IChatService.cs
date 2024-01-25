using RPS.Domain.Entities;

namespace RPS.Domain.Services.Abstractions.Chat;

public interface IChatService
{
    public Task<GameRoom> GetChatById(string gameRoomId);
}