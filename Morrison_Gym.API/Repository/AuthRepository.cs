using Microsoft.EntityFrameworkCore;
using Morrison_Gym.API.Data;
using Morrison_Gym.API.Entities;
using Morrison_Gym.API.Repository.Contract;

namespace Morrison_Gym.API.Repository;

public class AuthRepository : BaseRepository<User>, IAuthRepository
{
    public AuthRepository(DataContext dataContext) : base(dataContext) { }

    public async Task<User> Login(Guid code)
    {
        return (await FindByCondition(x => x.UserCode == code)
            .Include(r => r.Role)
            .SingleOrDefaultAsync())!;
    }

    public void CreateUser(User user)
    {
        Create(user);
    }
}