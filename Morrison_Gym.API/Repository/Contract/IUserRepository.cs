using Morrison_Gym.API.Entities;

namespace Morrison_Gym.API.Repository.Contract
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int userId);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUserAsync(User user);
    }
}
