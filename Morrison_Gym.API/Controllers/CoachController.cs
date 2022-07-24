using Microsoft.AspNetCore.Mvc;
using Morrison_Gym.API.Models.Dto;
using Morrison_Gym.API.Services.CoachService;

namespace Morrison_Gym.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoachController : ControllerBase
    {
        private readonly ICoachService _coachService;
        private ResponseDto _responseDto;

        public CoachController(ICoachService coachService)
        {
            _coachService = coachService;
            _responseDto = new ResponseDto();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                _responseDto = await _coachService.GetCoaches();
            }
            catch
            {
                _responseDto.Success = false;
                return NotFound(_responseDto);
            }
            return Ok(_responseDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                _responseDto = await _coachService.GetCoachData(id);
            }
            catch (Exception ex)
            {
                _responseDto.Success = false;
                _responseDto.ErrorMessages = new List<string>() { ex.ToString() };
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddCoach(CoachDto coachDto)
        {
            try
            {
                _responseDto = await _coachService.AddCoach(coachDto);
            }
            catch (Exception ex)
            {
                _responseDto.Success = false;
                _responseDto.ErrorMessages = new List<string>() { ex.ToString() };
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCoach(CoachDto coachDto)
        {
            try
            {
                _responseDto = await _coachService.UpdateCoach(coachDto);
            }
            catch (Exception ex)
            {
                _responseDto.Success = false;
                _responseDto.ErrorMessages = new List<string>() { ex.ToString() };
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCoach(int id)
        {
            try
            {
                _responseDto.Success = await _coachService.DeleteCoach(id);
            }
            catch (Exception ex)
            {
                _responseDto.Success = false;
                _responseDto.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }

    }
}
