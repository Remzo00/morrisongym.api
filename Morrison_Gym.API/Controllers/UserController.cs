using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Morrison_Gym.API.Dto;
using Morrison_Gym.API.Entities;
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
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
            _responseDto = new ResponseDto();
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Coach")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var users = await _userService.GetUsers();
                _responseDto.Result = _mapper.Map<IList<UserDto>>(users);
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
                var user = await _userService.GetUsers();
                _responseDto.Result = _mapper.Map<UserDto>(user);
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
        public async Task<IActionResult> AddUser(UserCreateDto userDto)
        {
            try
            {
                if (userDto == null)
                {
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var user = _mapper.Map<User>(userDto);
                _responseDto.Result = await _userService.AddUser(user);
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
        public async Task<IActionResult> UpdateUser(int id,UserUpdateDto userDto)
        {
            try
            {
                if (id < 1 || userDto == null || id != userDto.Id)
                {
                    return BadRequest();
                }
                var isExists = await _userService.isExists(id);
                if (!isExists)
                {
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var user = _mapper.Map<User>(userDto);
                _responseDto.Result = await _userService.UpdateUser(user);
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
        [HttpGet("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                if (id < 1)
                {
                    return BadRequest();
                }
                var isExists = await _userService.isExists(id);
                if (!isExists)
                {
                    return NotFound();
                }
                var user = await _userService.GetUserById(id);
                var userForDelete = _mapper.Map<User>(user.Result);
                _responseDto.Success = await _userService.DeleteUser(userForDelete);
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
