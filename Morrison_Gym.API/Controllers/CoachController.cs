using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Morrison_Gym.API.Dto;
using Morrison_Gym.API.Entities;
using Morrison_Gym.API.Services.CoachService;

namespace Morrison_Gym.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoachController : ControllerBase
    {
        private readonly ICoachService _coachService;
        private ResponseDto _responseDto;
        private readonly IMapper _mapper;

        public CoachController(ICoachService coachService, IMapper mapper)
        {
            _coachService = coachService;
            _responseDto = new ResponseDto();
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var coaches = await _coachService.GetCoaches();
                _responseDto.Result = _mapper.Map<IList<CoachDto>>(coaches);
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
                var coach = await _coachService.GetCoaches();
                _responseDto.Result = _mapper.Map<CoachDto>(coach);
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
        public async Task<IActionResult> AddCoach(CoachCreateDto coachDto)
        {
            try
            {
                if (coachDto == null)
                {
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var coach = _mapper.Map<Coach>(coachDto);
                _responseDto.Result = await _coachService.AddCoach(coach);
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
        [HttpGet("{id}")]
        public async Task<IActionResult> UpdateCoach(int id, CoachUpdateDto coachDto)
        {
            try
            {
                if (id < 1 || coachDto == null || id != coachDto.Id)
                {
                    return BadRequest();
                }
                var isExists = await _coachService.isExists(id);
                if (!isExists)
                {
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var coach = _mapper.Map<Coach>(coachDto);
                _responseDto.Result = await _coachService.UpdateCoach(coach);
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
        [HttpGet("{id}")]
        public async Task<IActionResult> DeleteCoach(int id)
        {
            try
            {
                if (id < 1)
                {
                    return BadRequest();
                }
                var isExists = await _coachService.isExists(id);
                if (!isExists)
                {
                    return NotFound();
                }
                var coach = await _coachService.GetCoachById(id);
                var coachForDelete = _mapper.Map<Coach>(coach.Result);
                _responseDto.Success = await _coachService.DeleteCoach(coachForDelete);
                return NoContent();
            }
            catch (Exception ex)
            {
                _responseDto.Success = false;
                _responseDto.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_responseDto);
            }
        }

    }
}
