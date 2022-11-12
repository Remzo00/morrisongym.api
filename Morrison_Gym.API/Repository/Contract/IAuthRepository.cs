using Morrison_Gym.API.Entities;

namespace Morrison_Gym.API.Repository.Contract
{
    public interface IAuthRepository : IBaseRepository<User>
    {
        Task<User> Login(Guid code);
        void CreateUser(User user);
    }
}
