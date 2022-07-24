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
<<<<<<< HEAD:Services/AuthService/AuthService.cs
                User? user = await _dataContext.Users.SingleOrDefaultAsync(x => x.UserCode == code);
=======
                var user = await _dataContext.Users.SingleOrDefaultAsync(x => x.UserCode == code);
>>>>>>> main:Morrison_Gym.API/Services/AuthService/AuthService.cs
                if(user == null)
                {
                    response.Success = false;
                    response.Message = "User not found";
                }

                if (user != null) response.Result = await CreateToken(user);
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
                user.Role = await _dataContext.Roles.SingleOrDefaultAsync((x => x.Id == user.RoleId));
                user.UserCode = Guid.NewGuid();
                _dataContext.Users.Add(user);
                await _dataContext.SaveChangesAsync();
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessages = new List<string> { ex.ToString() };
                return response;
            }
        }

        private async Task<string> CreateToken(User user)
        {
<<<<<<< HEAD:Services/AuthService/AuthService.cs
            var role = await _dataContext.Roles.FindAsync(user.RoleId);
            List<Claim> claims = new List<Claim>
=======
            var role = await _dataContext.Roles.SingleOrDefaultAsync(x => x.Id == user.RoleId);
            var claims = new List<Claim>
>>>>>>> main:Morrison_Gym.API/Services/AuthService/AuthService.cs
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
            };

            if (role != null) claims.Add(new Claim(ClaimTypes.Role, role.Name));

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value));

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = signingCredentials,
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
