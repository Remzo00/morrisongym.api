using Morrison_Gym.API.Models;
using Morrison_Gym.API.Models.Dto;

namespace Morrison_Gym.API.Services.CoachService
{
    public interface ICoachService
    {
        Task<ResponseDto> GetCoaches();
        Task<ResponseDto> GetCoachData(int id);
        Task<ResponseDto> AddCoach(CoachDto request);
        Task<ResponseDto> UpdateCoach(CoachDto request);
        Task<bool> DeleteCoach(int id);
    }
}
