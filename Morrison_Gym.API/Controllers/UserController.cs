using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Morrison_Gym.API.Dto;
using Morrison_Gym.API.Models.Dto;
using Morrison_Gym.API.Services.UserService;

namespace Morrison_Gym.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private ResponseDto _responseDto;

        public UserController(IUserService userService)
        {
            _userService = userService;
            _responseDto = new ResponseDto();
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Coach")]
        public async Task<IActionResult> Get()
        {
            try
            {
                _responseDto = await _userService.GetUsers();
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
                _responseDto = await _userService.GetUserData(id);
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
        public async Task<IActionResult> AddUser(UserDto userDto)
        {
            try
            {
                _responseDto = await _userService.AddUser(userDto);
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
        public async Task<IActionResult> UpdateUser(UserDto userDto)
        {
            try
            {
                _responseDto = await _userService.UpdateUser(userDto);
            }
            catch(Exception ex)
            {
                _responseDto.Success = false;
                _responseDto.ErrorMessages = new List<string>() { ex.ToString() };
                return BadRequest(_responseDto);               
            }
            return Ok(_responseDto);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                _responseDto.Success = await _userService.DeleteUser(id);
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
