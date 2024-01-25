namespace RPS.Domain.Repositories.Abstractions;

public interface IRepositoryManager
{
    IGameRoomRepository RoomRepository { get; }
    IMessageRepository MessageRepository { get; }
    IUnitOfWork UnitOfWork { get; }
}