using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Morrison_Gym.API.Data;
using Morrison_Gym.API.Models;
using Morrison_Gym.API.Models.Dto;

namespace Morrison_Gym.API.Services.CustomerService
{
    public class CustomerService : ICustomerService
    {
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;
        public CustomerService(DataContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        //Get all Customers
        public async Task<ResponseDto> GetCustomers()
        {
            ResponseDto response = new();
            try
            {                
                var customers = await _dbContext.Customers.ToListAsync();
                response.Success = true;
                response.Result = _mapper.Map<List<CustomerDto>>(customers);
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessages = new List<string>() { ex.ToString() };
                throw;
            }
        }
        //Get particular Customer
        public async Task<ResponseDto> GetCustomerData(int id)
        {
            ResponseDto response = new();
            try
            {                
                var customer = await _dbContext.Customers.FirstAsync(x => x.Id == id);
                if (customer == null)
                {
                    response.Message = "User not found.";
                }
                response.Success = true;
                response.Result = _mapper.Map<CustomerDto>(customer);
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessages = new List<string>() { ex.ToString() };
                throw;
            }
        }
        //Add Customer
        public async Task<ResponseDto> AddCustomer(CustomerDto request)
        {
            ResponseDto response = new();
            Customer customer = _mapper.Map<Customer>(request);
            try
            {
                response.Success = true;                
                _dbContext.Customers?.Add(customer);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessages = new List<string>() { ex.ToString() };
                throw;
            }
            return response;
        }
        //Update Customer
        public async Task<ResponseDto> UpdateCustomer(CustomerDto request)
        {
            ResponseDto response = new();
            Customer customer = _mapper.Map<Customer>(request);
            try
            {
                if (customer != null)
                {
                    response.Success = true;
                    _dbContext.Customers?.Update(customer);
                    await _dbContext.SaveChangesAsync();
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessages = new List<string>() { ex.ToString() };
                throw;
            }
            return response;
        }
        //Delete Customer
        public async Task<bool> DeleteCustomer(int id)
        {            
            try
            {
                var customer = _dbContext.Customers?.First(x => x.Id == id);
                if (customer != null)
                {
                    _dbContext.Customers?.Remove(customer);
                    await _dbContext.SaveChangesAsync();                 
                    return true;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }     
       
    }
}
