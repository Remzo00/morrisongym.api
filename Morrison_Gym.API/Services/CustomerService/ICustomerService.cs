using Morrison_Gym.API.Dto;
using Morrison_Gym.API.Entities;
using Morrison_Gym.API.Models.Dto;

namespace Morrison_Gym.API.Services.CustomerService
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllAsync();
        Task<CustomerDto> GetByIdAsync(int customerId);
        Task<ResponseDto> CreateAsync(CustomerCreateDto customerCreateDto);
        Task<ResponseDto> UpdateAsync(int customerId, CustomerUpdateDto customerUpdateDto);
        Task<bool> DeleteAsync(int customerId);
    }
}
