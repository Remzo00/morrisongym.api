using Morrison_Gym.API.Repository.Contract;
using Morrison_Gym.API.Services.CoachService;

namespace Morrison_Gym.API.Services;
public class ServiceManager : IServiceManager
{
    private readonly Lazy<ICoachService> _lazyCoachService;

    public ServiceManager(IRepositoryManager repositoryManager)
    {
        _lazyCoachService = new Lazy<ICoachService>(() => new CoachService.CoachService(repositoryManager)); ;
    }

    public ICoachService CoachService => _lazyCoachService.Value;
}