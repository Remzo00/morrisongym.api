using Mapster;
using Morrison_Gym.API.Dto;
using Morrison_Gym.API.Entities;
using Morrison_Gym.API.Repository.Contract;

namespace Morrison_Gym.API.Services.CoachService
{
    public class CoachService : ICoachService
    {
        private readonly ResponseDto _response;
        private readonly IRepositoryManager _repositoryManager;

        public CoachService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
            _response = new ResponseDto();
        }

        public async Task<IEnumerable<CoachDto>> GetAllAsync()
        {
            var coaches = await _repositoryManager.CoachRepository.GetAllAsync();

            return coaches.Adapt<IEnumerable<CoachDto>>();
        }

        public async Task<CoachDto> GetByIdAsync(int coachId)
        {
            var coach = await _repositoryManager.CoachRepository.GetByIdAsync(coachId);
            return coach.Adapt<CoachDto>();
        }

        public async Task<ResponseDto> CreateAsync(CoachCreateDto coachCreateDto)
        {
            var coach = coachCreateDto.Adapt<Coach>();
            _repositoryManager.CoachRepository.CreateCoach(coach);
            var result = await _repositoryManager.UnitOfWork.SaveChangesAsync();
            if (result != 0) return _response;
            _response.Success = false;
            _response.Message = "Error Creating Coach";
            return _response;
        }

        public async Task<ResponseDto> UpdateAsync(int coachId, CoachUpdateDto coachUpdateDto)
        {
            var coachCheck = await _repositoryManager.CoachRepository.GetByIdAsync(coachId);
            if (coachCheck is null)
            {
                _response.Success = false;
                _response.Message = "Coach not found in database";
                return _response;
            }
            var coach = coachUpdateDto.Adapt<Coach>();
            _repositoryManager.CoachRepository.UpdateCoach(coach);

            var result = await _repositoryManager.UnitOfWork.SaveChangesAsync();
            if (result > 0) return _response;
            _response.Success = false;
            _response.Message = "Error Updating Coach";
            return _response;
        }

        public async Task<bool> DeleteAsync(int coachId)
        {
            var coach = await _repositoryManager.CoachRepository.GetByIdAsync(coachId);
            _repositoryManager.CoachRepository.DeleteCoachAsync(coach);
            return await _repositoryManager.UnitOfWork.SaveChangesAsync() == 1;
        }
    }
}