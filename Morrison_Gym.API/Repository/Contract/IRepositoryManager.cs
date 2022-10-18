namespace Morrison_Gym.API.Repository.Contract;

public interface IRepositoryManager
{
    IUnitOfWork UnitOfWork { get; }
    ICoachRepository CoachRepository { get; }
}