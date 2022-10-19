using Morrison_Gym.API.Dto;

namespace Morrison_Gym.API.Services.CoachService
{
    public interface ICoachService
    {
        Task<IEnumerable<CoachDto>> GetAllAsync();
        Task<CoachDto> GetByIdAsync(int coachId);
        Task<ResponseDto> CreateAsync(CoachCreateDto coachCreateDto);
        Task<ResponseDto> UpdateAsync(int coachId, CoachUpdateDto coachUpdateDto);
        Task<bool> DeleteAsync(int coachId);
    }
}
