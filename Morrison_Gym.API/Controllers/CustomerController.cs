using Microsoft.AspNetCore.Mvc;
using Morrison_Gym.API.Dto;
using Morrison_Gym.API.Entities;
using Morrison_Gym.API.Models.Dto;
using Morrison_Gym.API.Services;
using Morrison_Gym.API.Services.CustomerService;

namespace Morrison_Gym.API.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private ResponseDto _response;
        public CustomerController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
            _response = new ResponseDto();
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomer()
        {
            try
            {
                var customers = await _serviceManager.CustomerService.GetAllAsync();
                return Ok(customers);
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
                var customer = await _serviceManager.CustomerService.GetByIdAsync(id);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return NotFound(_response);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CustomerCreateDto customerCreateDto)
        {
            try
            {
                if (customerCreateDto is null)
                {
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _response = await _serviceManager.CustomerService.CreateAsync(customerCreateDto);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return NotFound(_response);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCustomer(int id, CustomerUpdateDto customerUpdateDto)
        {
            try
            {
                if (id < 1 || customerUpdateDto == null || id != customerUpdateDto.Id)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _response = await _serviceManager.CustomerService.UpdateAsync(id, customerUpdateDto);
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
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                if (id < 1)
                {
                    return BadRequest();
                }
                _response.Success = await _serviceManager.CustomerService.DeleteAsync(id);

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
