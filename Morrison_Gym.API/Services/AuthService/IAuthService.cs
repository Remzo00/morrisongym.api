using Morrison_Gym.API.Dto;
using Morrison_Gym.API.Models.Dto;

namespace Morrison_Gym.API.Services.AuthService
{
    public interface IAuthService
    {
        Task<ResponseDto> Login(Guid code);
        Task<ResponseDto> Register(UserDto userDto);
    }
}
