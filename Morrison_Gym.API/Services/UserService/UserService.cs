using Microsoft.EntityFrameworkCore;
using Morrison_Gym.API.Controllers;
using Morrison_Gym.API.Data;
using Morrison_Gym.API.Dto;
using Morrison_Gym.API.Entities;

namespace Morrison_Gym.API.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly DataContext _dbContext;
        public UserService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        //To get all users details
        public async Task<ResponseDto> GetUsers()
        {
            ResponseDto response = new();
            try
            {
                response.Result = await _dbContext.Users.ToListAsync();
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessages = new List<string>() { ex.ToString() };
                throw;
            }
        }
        //Get the details of a particular user
        public async Task<ResponseDto> GetUserById(int id)
        {
            ResponseDto response = new();
            try
            {
                var user = await _dbContext.Users.FirstAsync(x => x.Id == id);
                if (user == null)
                {
                    response.Message = "User not found.";
                }
                response.Result = user;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessages = new List<string>() { ex.ToString() };
                throw;
            }
        }
        //To add new user
        public async Task<ResponseDto> AddUser(User entity)
        {
            ResponseDto response = new();
            try
            {
                _dbContext.Users.Add(entity);
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
        //Deleting user
        public async Task<bool> DeleteUser(User entity)
        {
            try
            {
                _dbContext.Users.Remove(entity);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResponseDto> UpdateUser(User entity)
        {
            ResponseDto response = new();
            try
            {
                _dbContext.Users.Update(entity);
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


        public async Task<bool> isExists(int id)
        {
            var isExists = await _dbContext.Users.AnyAsync(x => x.Id == id);
            return isExists;
        }
    }
}
