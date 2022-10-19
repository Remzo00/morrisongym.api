using Morrison_Gym.API.Data;
using Morrison_Gym.API.Repository.Contract;

namespace Morrison_Gym.API.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _dbContext;
    public UnitOfWork(DataContext dbContext) => _dbContext = dbContext;
    public Task<int> SaveChangesAsync() => _dbContext.SaveChangesAsync();
}