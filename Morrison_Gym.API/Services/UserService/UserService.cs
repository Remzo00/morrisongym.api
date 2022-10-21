using Mapster;
using Microsoft.EntityFrameworkCore;
using Morrison_Gym.API.Controllers;
using Morrison_Gym.API.Data;
using Morrison_Gym.API.Dto;
using Morrison_Gym.API.Entities;
using Morrison_Gym.API.Models.Dto;
using Morrison_Gym.API.Repository.Contract;

namespace Morrison_Gym.API.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly ResponseDto _response;
        private readonly IRepositoryManager _repositoryManager;
        public UserService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
            _response = new ResponseDto();
        }
        //To get all users details
        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _repositoryManager.UserRepository.GetAllAsync();
            return users.Adapt<IEnumerable<UserDto>>();
        }
        //Get the details of a particular user
        public async Task<UserDto> GetByIdAsync(int userId)
        {
            var users = await _repositoryManager.UserRepository.GetAllAsync();

            return users.Adapt<UserDto>();
        }
        //To add new user
        public async Task<ResponseDto> CreateAsync(UserCreateDto userCreateDto)
        {
            var user = userCreateDto.Adapt<User>();
            _repositoryManager.UserRepository.CreateUser(user);
            var result = await _repositoryManager.UnitOfWork.SaveChangesAsync();
            if (result != 0) return _response;
            _response.Success = false;
            _response.Message = "Error Creating User";
            return _response;
        }
        //Updating user
        public async Task<ResponseDto> UpdateAsync(int userId, UserUpdateDto userUpdateDto)
        {
            var userCheck = await _repositoryManager.UserRepository.GetByIdAsync(userId);
            if (userCheck is null)
            {
                _response.Success = false;
                _response.Message = "User not found in database";
                return _response;
            }
            var user = userUpdateDto.Adapt<User>();
            _repositoryManager.UserRepository.UpdateUser(user);

            var result = await _repositoryManager.UnitOfWork.SaveChangesAsync();
            if (result > 0) return _response;
            _response.Success = false;
            _response.Message = "Error Updating User";
            return _response;
        }

        public async Task<bool> DeleteAsync(int userId)
        {
            var user = await _repositoryManager.UserRepository.GetByIdAsync(userId);
            _repositoryManager.UserRepository.DeleteUserAsync(user);
            return await _repositoryManager.UnitOfWork.SaveChangesAsync() == 1;
        }
    }
}
