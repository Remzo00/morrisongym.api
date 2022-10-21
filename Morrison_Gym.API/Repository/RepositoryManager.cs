using Morrison_Gym.API.Data;
using Morrison_Gym.API.Repository.Contract;

namespace Morrison_Gym.API.Repository;

public class RepositoryManager : IRepositoryManager
{
    private readonly Lazy<IUnitOfWork> _lazyUnitOfWork;
    private readonly Lazy<ICoachRepository> _lazyCoachRepository;
    private readonly Lazy<IUserRepository> _lazyUserRepository;
    private readonly Lazy<ICustomerRepository> _lazyCustomerRepository;

    public RepositoryManager(DataContext dbContext)
    {
        _lazyUnitOfWork = new Lazy<IUnitOfWork>(() => new UnitOfWork(dbContext));
        _lazyCoachRepository = new Lazy<ICoachRepository>(() => new CoachRepository(dbContext));
        _lazyUserRepository = new Lazy<IUserRepository>(() => new UserRepository(dbContext));
        _lazyCustomerRepository = new Lazy<ICustomerRepository>(() => new CustomerRepository(dbContext));
    }
    public IUnitOfWork UnitOfWork => _lazyUnitOfWork.Value;
    public ICoachRepository CoachRepository => _lazyCoachRepository.Value;
    public IUserRepository UserRepository => _lazyUserRepository.Value;
    public ICustomerRepository CustomerRepository => _lazyCustomerRepository.Value;
}