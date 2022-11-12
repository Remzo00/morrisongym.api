using Microsoft.AspNetCore.Mvc;
using Morrison_Gym.API.Dto;
using Morrison_Gym.API.Models.Dto;
using Morrison_Gym.API.Services;

namespace Morrison_Gym.API.Controllers
{
    [Route("api/user")] 
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private ResponseDto _response;

        public UserController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
            _response = new ResponseDto();
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await _serviceManager.UserService.GetAllAsync();
                return Ok(users);
            }
            catch (Exception ex)
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
                var user = await _serviceManager.UserService.GetByIdAsync(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return NotFound(_response);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateUser(int id, UserUpdateDto userUpdateDto)
        {
            try
            {
                if (id < 1 || userUpdateDto == null || id != userUpdateDto.Id)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _response = await _serviceManager.UserService.UpdateAsync(id, userUpdateDto);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return NotFound(_response);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                if (id < 1)
                {
                    return BadRequest();
                }
                _response.Success = await _serviceManager.UserService.DeleteAsync(id);

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
