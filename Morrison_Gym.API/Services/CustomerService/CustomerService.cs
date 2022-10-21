using Mapster;
using Microsoft.EntityFrameworkCore;
using Morrison_Gym.API.Data;
using Morrison_Gym.API.Dto;
using Morrison_Gym.API.Entities;
using Morrison_Gym.API.Models.Dto;
using Morrison_Gym.API.Repository.Contract;

namespace Morrison_Gym.API.Services.CustomerService
{
    public class CustomerService : ICustomerService
    {
        private readonly ResponseDto _response;
        private readonly IRepositoryManager _repositoryManager;
        public CustomerService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
            _response = new ResponseDto();
        }

        //Get all Customers
        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            var customers = await _repositoryManager.CustomerRepository.GetAllAsync();
            return customers.Adapt<IEnumerable<CustomerDto>>();
        }
        //Get particular Customer
        public async Task<CustomerDto> GetByIdAsync(int customerId)
        {
            var customers = await _repositoryManager.CustomerRepository.GetAllAsync();

            return customers.Adapt<CustomerDto>();
        }
        //Add Customer
        public async Task<ResponseDto> CreateAsync(CustomerCreateDto customerCreateDto)
        {
            var customer = customerCreateDto.Adapt<Customer>();
            _repositoryManager.CustomerRepository.CreateCustomer(customer);
            var result = await _repositoryManager.UnitOfWork.SaveChangesAsync();
            if (result != 0) return _response;
            _response.Success = false;
            _response.Message = "Error Creating Customer";
            return _response;
        }
        //Update Customer
        public async Task<ResponseDto> UpdateAsync(int customerId, CustomerUpdateDto customerUpdateDto)
        {
            var customerCheck = await _repositoryManager.CustomerRepository.GetByIdAsync(customerId);
            if (customerCheck is null)
            {
                _response.Success = false;
                _response.Message = "Customer not found in database";
                return _response;
            }
            var customer = customerUpdateDto.Adapt<Customer>();
            _repositoryManager.CustomerRepository.UpdateCustomer(customer);

            var result = await _repositoryManager.UnitOfWork.SaveChangesAsync();
            if (result > 0) return _response;
            _response.Success = false;
            _response.Message = "Error Updating Customer";
            return _response;
        }
        //Delete Customer
        public async Task<bool> DeleteAsync(int customerId)
        {
            var customer = await _repositoryManager.CustomerRepository.GetByIdAsync(customerId);
            _repositoryManager.CustomerRepository.DeleteCustomerAsync(customer);
            return await _repositoryManager.UnitOfWork.SaveChangesAsync() == 1;
        }
    }
}
