namespace RPS.Domain.Repositories.Abstractions;

public interface IRepositoryManager
{
    IGameRoomRepository RoomRepository { get; }
    IMessageRepository MessageRepository { get; }
    IMatchRepository MatchRepository { get; }
    IUnitOfWork UnitOfWork { get; }
}