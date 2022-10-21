using Morrison_Gym.API.Controllers;
using Morrison_Gym.API.Services.CoachService;
using Morrison_Gym.API.Services.CustomerService;

namespace Morrison_Gym.API.Services;

public interface IServiceManager
{
    ICoachService CoachService { get; }
    IUserService UserService { get; }
    ICustomerService CustomerService { get; }
}