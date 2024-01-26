using RPS.Domain.Repositories.Abstractions;

namespace RPS.Infrastructure.Database.Repositories;

public sealed class RepositoryManager : IRepositoryManager
{
    private readonly Lazy<IUnitOfWork> _lazyUnitOfWork;
    private readonly Lazy<IGameRoomRepository> _lazyGameRoomRepository;
    private readonly Lazy<IMessageRepository> _lazyMessageRepository;
    private readonly Lazy<IRoundRepository> _lazyMatchRepository;
    
    public RepositoryManager(ApplicationDbContext dbContext)
    {
        _lazyMatchRepository = new Lazy<IRoundRepository>(() => new RoundRepository(dbContext));
        _lazyUnitOfWork = new Lazy<IUnitOfWork>(() => new UnitOfWork(dbContext));
        _lazyMessageRepository = new Lazy<IMessageRepository>(() => new MessageRepository(dbContext));
        _lazyGameRoomRepository = new Lazy<IGameRoomRepository>(() => new GameRoomRepository(dbContext));
    }
    
    public IGameRoomRepository GameRoomRepository => _lazyGameRoomRepository.Value;
    public IMessageRepository MessageRepository => _lazyMessageRepository.Value;
    public IRoundRepository RoundRepository => _lazyMatchRepository.Value;
    public IUnitOfWork UnitOfWork => _lazyUnitOfWork.Value;
}

