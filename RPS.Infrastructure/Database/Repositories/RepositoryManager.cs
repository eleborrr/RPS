using RPS.Domain.Repositories.Abstractions;

namespace RPS.Infrastructure.Database.Repositories;

public sealed class RepositoryManager : IRepositoryManager
{
    private readonly Lazy<IUnitOfWork> _lazyUnitOfWork;
    private readonly Lazy<IGameRoomRepository> _lazyRoomRepository;
    private readonly Lazy<IMessageRepository> _lazyMessageRepository;
    public RepositoryManager(ApplicationDbContext dbContext)
    {
        _lazyUnitOfWork = new Lazy<IUnitOfWork>(() => new UnitOfWork(dbContext));
        _lazyMessageRepository = new Lazy<IMessageRepository>(() => new MessageRepository(dbContext));
        _lazyRoomRepository = new Lazy<IGameRoomRepository>(() => new GameRoomRepository(dbContext));

    }
    
    public IGameRoomRepository RoomRepository => _lazyRoomRepository.Value;
    public IMessageRepository MessageRepository => _lazyMessageRepository.Value;
    public IUnitOfWork UnitOfWork => _lazyUnitOfWork.Value;
}

