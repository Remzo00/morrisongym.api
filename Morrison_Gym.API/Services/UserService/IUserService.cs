using Morrison_Gym.API.Dto;
using Morrison_Gym.API.Models.Dto;

namespace Morrison_Gym.API.Controllers
{
    public interface IUserService
    { 
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> GetByIdAsync(int userId);
        Task<ResponseDto> UpdateAsync(int userId, UserUpdateDto userUpdateDto);
        Task<bool> DeleteAsync(int userId);
    }
}