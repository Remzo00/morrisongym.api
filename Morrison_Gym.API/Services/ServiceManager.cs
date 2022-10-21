﻿using Morrison_Gym.API.Controllers;
using Morrison_Gym.API.Repository.Contract;
using Morrison_Gym.API.Services.CoachService;
using Morrison_Gym.API.Services.CustomerService;

namespace Morrison_Gym.API.Services;
public class ServiceManager : IServiceManager
{
    private readonly Lazy<ICoachService> _lazyCoachService;
    private readonly Lazy<IUserService> _lazyUserService;
    private readonly Lazy<ICustomerService> _lazyCustomerService;

    public ServiceManager(IRepositoryManager repositoryManager)
    {
        _lazyCoachService = new Lazy<ICoachService>(() => new CoachService.CoachService(repositoryManager)); ;
        _lazyUserService = new Lazy<IUserService>(() => new UserService.UserService(repositoryManager));
        _lazyCustomerService = new Lazy<ICustomerService>(() => new CustomerService.CustomerService(repositoryManager));
    }

    public ICoachService CoachService => _lazyCoachService.Value;
    public IUserService UserService => _lazyUserService.Value;
    public ICustomerService CustomerService => _lazyCustomerService.Value;
}