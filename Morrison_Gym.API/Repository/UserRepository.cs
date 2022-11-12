using Microsoft.EntityFrameworkCore;
using Morrison_Gym.API.Data;
using Morrison_Gym.API.Entities;
using Morrison_Gym.API.Repository.Contract;

namespace Morrison_Gym.API.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DataContext dataContext) : base(dataContext) { }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await FindAll().Include(x => x.Id).ToListAsync();
        }

        public async Task<User> GetByIdAsync(int userId)
        {
            return (await FindByCondition(x => x.Id == userId).Include(u => u.Id).SingleOrDefaultAsync())!;
        }

        public void UpdateUser(User user)
        {
            Update(user);
        }

        public void DeleteUserAsync(User user)
        {
            Delete(user);
        }
    }
}
