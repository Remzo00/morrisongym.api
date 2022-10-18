namespace Morrison_Gym.API.Repository.Contract;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}