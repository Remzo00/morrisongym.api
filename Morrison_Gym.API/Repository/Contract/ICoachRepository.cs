using Morrison_Gym.API.Entities;

namespace Morrison_Gym.API.Repository.Contract
{
    public interface ICoachRepository : IBaseRepository<Coach>
    {
        Task<IEnumerable<Coach>> GetAllAsync();
        Task<Coach> GetByIdAsync(int coachId);
        void CreateCoach(Coach coach);
        void UpdateCoach(Coach coach);
        void DeleteCoachAsync(Coach coach);
    }
}
