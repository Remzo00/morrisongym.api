using Morrison_Gym.API.Dto;
using Morrison_Gym.API.Entities;

namespace Morrison_Gym.API.Controllers
{
    public interface IUserService
    {
        Task<ResponseDto> GetUsers();
        Task<ResponseDto> GetUserById(int id);
        Task<ResponseDto> AddUser(User entity);
        Task<ResponseDto> UpdateUser(User entity);
        Task<bool> DeleteUser(User entity);
        Task<bool> isExists(int id);
    }
}