using Morrison_Gym.API.Dto;
using Morrison_Gym.API.Models.Dto;

namespace Morrison_Gym.API.Services.UserService
{
    public interface IUserService
    {
        Task<ResponseDto> GetUsers();
        Task<ResponseDto> GetUserData(int id);
        Task<ResponseDto> AddUser(UserDto request);
        Task<ResponseDto> UpdateUser(UserDto request);
        Task<bool> DeleteUser(int id);
    }
}
