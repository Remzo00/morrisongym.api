using Microsoft.AspNetCore.Mvc;
using Morrison_Gym.API.Dto;
using Morrison_Gym.API.Models.Dto;
using Morrison_Gym.API.Services;

namespace Morrison_Gym.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private ResponseDto _response;

        public AuthController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
            _response = new ResponseDto();
        }

        [HttpPost("registration")]
        public async Task<IActionResult> Register(UserRegisterDto registerDto)
        {
            try
            {
                if (registerDto is null)
                {
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _response = await _serviceManager.AuthService.Register(registerDto);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return NotFound(_response);
            }
        } 

        [HttpPost("login")]
        public async Task<IActionResult> Login(Guid userCode)
        {
            try
            {
                _response = await _serviceManager.AuthService.Login(userCode);
                return Ok(_response);
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
