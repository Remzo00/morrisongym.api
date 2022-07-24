using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Morrison_Gym.API.Data;
using Morrison_Gym.API.Models;
using Morrison_Gym.API.Models.Dto;

namespace Morrison_Gym.API.Services.CoachService
{
    public class CoachService : ICoachService
    {
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;

        public CoachService(DataContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;

        }

        //To get all coaches details
        public async Task<ResponseDto> GetCoaches()
        {
            ResponseDto response = new();
            try
            {                
                var coaches = await _dbContext.Coaches.ToListAsync();
                response.Success = true;
                response.Result = _mapper.Map<List<CoachDto>>(coaches);
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
        public async Task<ResponseDto> GetCoachData(int id)
        {
            ResponseDto response = new();
            try
            {                
                var coach = await _dbContext.Coaches.FirstAsync(x => x.Id == id);
                if (coach == null)
                {
                    response.Message = "User not found.";
                }
                response.Success = true;
                response.Result = _mapper.Map<CoachDto>(coach);
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
        public async Task<ResponseDto> AddCoach(CoachDto request)
        {
            ResponseDto response = new();
            try
            {                
                var coach = _mapper.Map<Coach>(request);
                response.Success = true;                
                _dbContext.Coaches?.Add(coach);
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
        public async Task<bool> DeleteCoach(int id)
        {            
            try
            {
                var coach = _dbContext.Coaches?.SingleOrDefault(x => x.Id == id);
                if (coach != null)
                {
                    _dbContext.Coaches?.Remove(coach);
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


        public async Task<ResponseDto> UpdateCoach(CoachDto request)
        {
            ResponseDto response = new();
            var coach = _mapper.Map<Coach>(request);
            try
            {
                if (coach != null)
                {
                    response.Success = true;
                    _dbContext.Coaches?.Update(coach);
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
    }
}