using Microsoft.EntityFrameworkCore;
using Morrison_Gym.API.Data;
using Morrison_Gym.API.Entities;
using Morrison_Gym.API.Repository.Contract;

namespace Morrison_Gym.API.Repository;

public class CoachRepository : BaseRepository<Coach>, ICoachRepository
{
    public CoachRepository(DataContext dataContext) : base(dataContext)  { }

    public async Task<IEnumerable<Coach>> GetAllAsync()
    {
        return await FindAll().Include(u => u.User).ToListAsync();
    }

    public async Task<Coach> GetByIdAsync(int coachId)
    {
        return (await FindByCondition(x => x.Id == coachId).Include(u => u.User).SingleOrDefaultAsync())!;
    }

    public void CreateCoach(Coach coach)
    {
        Create(coach);
    }

    public void UpdateCoach(Coach coach)
    {
        Update(coach);
    }

    public void DeleteCoachAsync(Coach coach)
    {
        Delete(coach);
    }
}