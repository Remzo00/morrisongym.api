using Morrison_Gym.API.Dto;
using Morrison_Gym.API.Entities;

namespace Morrison_Gym.API.Services.CoachService
{
    public interface ICoachService
    {
        Task<ResponseDto> GetCoaches();
        Task<ResponseDto> GetCoachById(int id);
        Task<ResponseDto> AddCoach(Coach entity);
        Task<ResponseDto> UpdateCoach(Coach entity);
        Task<bool> DeleteCoach(Coach entity);
        Task<bool> isExists(int id);
    }
}
