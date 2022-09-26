using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Morrison_Gym.API.Dto;
using Morrison_Gym.API.Entities;
using Morrison_Gym.API.Models.Dto;
using Morrison_Gym.API.Services.CustomerService;

namespace Morrison_Gym.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private ResponseDto _responseDto;
        private readonly IMapper _mapper;
        public CustomerController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _responseDto = new ResponseDto();
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var customers = await _customerService.GetCustomers();
                _responseDto.Result = _mapper.Map<IList<Customer>>(customers);
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
                var customer = await _customerService.GetCustomers();
                _responseDto.Result = _mapper.Map<CustomerDto>(customer);
            }
            catch(Exception ex)
            {
                _responseDto.Success = false;
                _responseDto.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_responseDto);
            }

            return Ok(_responseDto);
        }
        [HttpPost]
        public async Task<IActionResult> AddCustomer(CustomerCreateDto customerDto)
        {
            try
            {
                if (customerDto == null)
                {
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var customer = _mapper.Map<Customer>(customerDto);
                _responseDto.Result = await _customerService.AddCustomer(customer);
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
        public async Task<IActionResult> UpdateCustomer(int id, CustomerUpdateDto customerDto)
        {
            try
            {
                if (id < 1 || customerDto == null || id != customerDto.Id)
                {
                    return BadRequest();
                }
                var isExists = await _customerService.isExists(id);
                if (!isExists)
                {
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var customer = _mapper.Map<Customer>(customerDto);
                _responseDto.Result = await _customerService.UpdateCustomer(customer);
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
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                if (id < 1)
                {
                    return BadRequest();
                }
                var isExists = await _customerService.isExists(id);
                if (!isExists)
                {
                    return NotFound();
                }
                var customer = await _customerService.GetCustomerById(id);
                var customerForDelete = _mapper.Map<Customer>(customer.Result);
                _responseDto.Success = await _customerService.DeleteCustomer(customerForDelete);
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
