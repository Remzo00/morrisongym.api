using Morrison_Gym.API.Dto;
using Morrison_Gym.API.Entities;
using Morrison_Gym.API.Models.Dto;

namespace Morrison_Gym.API.Services.UserService
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
