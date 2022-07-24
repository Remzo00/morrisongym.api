using Morrison_Gym.API.Models.Dto;

namespace Morrison_Gym.API.Services.CustomerService
{
    public interface ICustomerService
    {
        Task<ResponseDto> GetCustomers();
        Task<ResponseDto> GetCustomerData(int id);
        Task<ResponseDto> AddCustomer(CustomerDto request);
        Task<ResponseDto> UpdateCustomer(CustomerDto request);
        Task<bool> DeleteCustomer(int id);
    }
}
