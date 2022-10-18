using Morrison_Gym.API.Services.CoachService;

namespace Morrison_Gym.API.Services;

public interface IServiceManager
{
    ICoachService CoachService { get; }
}