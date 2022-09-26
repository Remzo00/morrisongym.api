using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Morrison_Gym.API.Data;
using Morrison_Gym.API.Dto;
using Morrison_Gym.API.Entities;
using Morrison_Gym.API.Models.Dto;

namespace Morrison_Gym.API.Services.CustomerService
{
    public class CustomerService : ICustomerService
    {
        private readonly DataContext _dbContext;
        public CustomerService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        //Get all Customers
        public async Task<ResponseDto> GetCustomers()
        {
            ResponseDto response = new();
            try
            {     
                response.Result = await _dbContext.Customers.ToListAsync();
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
        public async Task<ResponseDto> GetCustomerById(int id)
        {
            ResponseDto response = new();
            try
            {                
                var customer = await _dbContext.Customers.FirstAsync(x => x.Id == id);
                if (customer == null)
                {
                    response.Message = "User not found.";
                }               
                response.Result = customer;
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
        public async Task<ResponseDto> AddCustomer(Customer entity)
        {
            ResponseDto response = new();            
            try
            {                                
                _dbContext.Customers.Add(entity);
                await _dbContext.SaveChangesAsync();
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessages = new List<string>() { ex.ToString() };
                throw;
            }
        }
        //Update Customer
        public async Task<ResponseDto> UpdateCustomer(Customer entity)
        {
            ResponseDto response = new();           
            try
            {
                _dbContext.Customers.Update(entity);
                await _dbContext.SaveChangesAsync();
                return response;               
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessages = new List<string>() { ex.ToString() };
                throw;
            }            
        }
        //Delete Customer
        public async Task<bool> DeleteCustomer(Customer entity)
        {            
            try
            {
                _dbContext.Customers.Remove(entity);
                await _dbContext.SaveChangesAsync();
                return true;                             
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }            
        }

        public async Task<bool> isExists(int id)
        {
            var isExists = await _dbContext.Customers.AnyAsync(x => x.Id == id);
            return isExists;
        }
    }
}
