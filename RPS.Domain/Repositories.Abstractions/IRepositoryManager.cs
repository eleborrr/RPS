namespace RPS.Domain.Repositories.Abstractions;

public interface IRepositoryManager
{
    IGameRoomRepository GameRoomRepository { get; }
    IMessageRepository MessageRepository { get; }
    IRoundRepository RoundRepository { get; }
    IUnitOfWork UnitOfWork { get; }
}