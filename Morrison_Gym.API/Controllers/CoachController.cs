using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using Morrison_Gym.API.Dto;
using Morrison_Gym.API.Entities;
using Morrison_Gym.API.Services;
using System.Threading;

namespace Morrison_Gym.API.Controllers
{
    [Route("api/coach")]
    [ApiController]
    public class CoachController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private ResponseDto _response;

        public CoachController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
            _response = new ResponseDto();
        }

        [HttpGet]
        public async Task<IActionResult> GetCoaches()
        {
            try
            {
                var coaches = await _serviceManager.CoachService.GetAllAsync();
                return Ok(coaches);
            }
            catch(Exception ex)
            {
                _response.Success = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return NotFound(_response);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var coach = await _serviceManager.CoachService.GetByIdAsync(id);
                return Ok(coach);
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return NotFound(_response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCoach(CoachCreateDto coachCreateDto)
        {
            try
            {
                if (coachCreateDto is null)
                {
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _response = await _serviceManager.CoachService.CreateAsync(coachCreateDto);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return NotFound(_response);
            }
        }

        [HttpPut]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> UpdateCoach(int id, CoachUpdateDto coachUpdateDto)
        {
            try
            {
                if (id < 1 || coachUpdateDto == null || id != coachUpdateDto.Id)
                {
                    return BadRequest();
                }
                
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _response = await _serviceManager.CoachService.UpdateAsync(id, coachUpdateDto);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return NotFound(_response);
            }
        }

        [HttpDelete]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> DeleteCoach(int id)
        {
            try
            {
                if (id < 1)
                {
                    return BadRequest();
                }
                _response.Success = await _serviceManager.CoachService.DeleteAsync(id);

                if (_response.Success)
                    return Ok(_response.Success);
                else
                    return NoContent();
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return NotFound(_response);
            }
        }

    }
}
