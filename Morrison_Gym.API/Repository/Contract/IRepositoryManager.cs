namespace Morrison_Gym.API.Repository.Contract;

public interface IRepositoryManager
{
    IUnitOfWork UnitOfWork { get; }
    ICoachRepository CoachRepository { get; }
    IUserRepository UserRepository { get; }
    ICustomerRepository CustomerRepository { get; }
    IAuthRepository AuthRepository { get; }
}