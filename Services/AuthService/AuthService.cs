using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Morrison_Gym.API.Data;
using Morrison_Gym.API.Models;
using Morrison_Gym.API.Models.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Morrison_Gym.API.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _dataContext;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthService(DataContext dataContext, IConfiguration configuration, IMapper mapper)
        {
            _dataContext = dataContext;
            _configuration = configuration;
            _mapper = mapper;
        }
        public async Task<ResponseDto> Login(Guid code)
        {
            ResponseDto response = new();
            try
            {
                User? user = await _dataContext.Users.FirstOrDefaultAsync(x => x.UserCode == code);
                if(user == null)
                {
                    response.Success = false;
                    response.Message = "User not found";
                }
                response.Result = await CreateToken(user);
                return response;
            }catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessages = new List<string> { ex.ToString() };
                return response;
            }
        }

        public async Task<ResponseDto> Register(UserDto userDto)
        {
            ResponseDto response = new();
            try
            {
                var user = _mapper.Map<User>(userDto);
                await _dataContext.Users.AddAsync(user);
                await _dataContext.SaveChangesAsync();
                return response;
            }catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessages = new List<string> { ex.ToString() };
                return response;
            }
        }

        private async Task<string> CreateToken(User user)
        {
            var role = await _dataContext.Admins.FindAsync(user.RoleId);
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                 new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, role.Name)
            };
            SymmetricSecurityKey key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value)
          );
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
