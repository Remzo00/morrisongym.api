using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Morrison_Gym.API.Data;
using Morrison_Gym.API.Dto;
using Morrison_Gym.API.Entities;

namespace Morrison_Gym.API.Services.CoachService
{
    public class CoachService : ICoachService
    {
        private readonly DataContext _dbContext;

        public CoachService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        //To get all coaches details
        public async Task<ResponseDto> GetCoaches()
        {
            ResponseDto response = new();
            try
            {
                response.Result = await _dbContext.Coaches.ToListAsync();
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessages = new List<string>() { ex.ToString() };
                throw;
            }            
        }

        //Get the details of a particular coach
        public async Task<ResponseDto> GetCoachById(int id)
        {
            ResponseDto response = new();
            try
            {                
                var coach = await _dbContext.Coaches.FirstAsync(x => x.Id == id);
                if (coach == null)
                {
                    response.Message = "User not found.";
                }
                response.Result = coach;
                return response;
            }
            catch (Exception ex)
            {
                response.Success= false;
                response.ErrorMessages = new List<string>() { ex.ToString() };
                throw;
            }
        }

        //Add Coach
        public async Task<ResponseDto> AddCoach(Coach entity)
        {
            ResponseDto response = new();
            try
            {                
                _dbContext.Coaches.Add(entity);
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

        //Deleting Coach
        public async Task<bool> DeleteCoach(Coach entity)
        {            
            try
            {
                _dbContext.Coaches.Remove(entity);
                await _dbContext.SaveChangesAsync();                   
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResponseDto> UpdateCoach(Coach entity)
        {            
            ResponseDto response = new();            
            try
            {
                _dbContext.Coaches.Update(entity);
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
            var isExists = await _dbContext.Coaches.AnyAsync(x => x.Id == id);
            return isExists;
        }
    }
}