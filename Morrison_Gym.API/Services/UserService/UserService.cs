using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Morrison_Gym.API.Data;
using Morrison_Gym.API.Dto;
using Morrison_Gym.API.Entities;
using Morrison_Gym.API.Models.Dto;

namespace Morrison_Gym.API.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;
        public UserService(DataContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        //To get all users details
        public async Task<ResponseDto> GetUsers()
        {
            ResponseDto response = new();
            try
            {              
                var users =  await _dbContext.Users.ToListAsync();
                response.Success = true;
                response.Result = _mapper.Map<List<UserDto>>(users);
                return response;
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.ErrorMessages = new List<string>() { ex.ToString() };
                throw;
            }
        }
        //Get the details of a particular user
        public async Task<ResponseDto> GetUserData(int id)
        {
            ResponseDto response = new();
            try
            {                
                var user = await _dbContext.Users.FirstAsync(x => x.Id == id);
                if (user == null)
                {
                    response.Message = "User not found.";
                }
                response.Success = true;
                response.Result = _mapper.Map<UserDto>(user);
                return response;
            }
            catch(Exception ex)
            {
                response.Success=false;
                response.ErrorMessages = new List<string>() { ex.ToString() };
                throw;
            }
        }
        //To add new user
        public async Task<ResponseDto> AddUser(UserDto request)
        {
            ResponseDto response = new();
            User user = _mapper.Map<User>(request);
            try
            {
                response.Success = true;               
                _dbContext.Users?.Add(user);
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
        //Deleting user
        public async Task<bool> DeleteUser(int id)
        {            
            try
            {
                var user = _dbContext.Users?.SingleOrDefault(x => x.Id == id);
                if (user != null)
                {
                    _dbContext.Users?.Remove(user);
                    await _dbContext.SaveChangesAsync();                    
                    return true;
                }                
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return false;
        }

        public async Task<ResponseDto> UpdateUser(UserDto request)
        {
            ResponseDto response = new();
            User user = _mapper.Map<User>(request);
            try
            {
                if(user != null)
                {
                    response.Success = true;
                    _dbContext.Users?.Update(user);
                    await _dbContext.SaveChangesAsync();
                    return response;
                }                
            }
            catch (Exception ex)
            {
                response.Success=false;
                response.ErrorMessages = new List<string>() { ex.ToString() };
                throw;
            }
            return response;
        }
    }
}
