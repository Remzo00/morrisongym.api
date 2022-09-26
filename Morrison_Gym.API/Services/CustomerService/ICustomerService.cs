using Morrison_Gym.API.Dto;
using Morrison_Gym.API.Entities;
using Morrison_Gym.API.Models.Dto;

namespace Morrison_Gym.API.Services.CustomerService
{
    public interface ICustomerService
    {
        Task<ResponseDto> GetCustomers();
        Task<ResponseDto> GetCustomerById(int id);
        Task<ResponseDto> AddCustomer(Customer entity);
        Task<ResponseDto> UpdateCustomer(Customer entity);
        Task<bool> DeleteCustomer(Customer entity);
        Task<bool> isExists(int id);         
    }
}
