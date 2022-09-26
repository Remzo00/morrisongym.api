using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Morrison_Gym.API.Dto;
using Morrison_Gym.API.Models.Dto;
using Morrison_Gym.API.Services.AuthService;

namespace Morrison_Gym.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected ResponseDto _response;

        public AuthController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            this._response = new ResponseDto();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserDto request)
        {
            try
            {
                _response = await _authService.Register(request);
            }catch (Exception ex)
            {
                _response.Success = false;
                _response.ErrorMessages = new List<string> { ex.ToString()};
            }
            return Ok(_response);
        } 

        [HttpPost("Login")]
        public async Task<IActionResult> Login(Guid request)
        {
            try
            {
                _response = await _authService.Login(request);
                if (!_response.Success)
                {
                    return BadRequest(_response);
                }
            }catch (Exception ex)
            {
                _response.Success = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return Ok(_response);
        }
    }
}
