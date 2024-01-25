namespace RPS.Domain.Repositories.Abstractions;

public interface IRepositoryManager
{
    IGameRoomRepository GameRoomRepository { get; }
    IMessageRepository MessageRepository { get; }
    IMatchRepository MatchRepository { get; }
    IUnitOfWork UnitOfWork { get; }
}