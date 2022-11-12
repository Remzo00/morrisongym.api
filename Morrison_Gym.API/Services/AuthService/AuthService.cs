using Mapster;
using Microsoft.IdentityModel.Tokens;
using Morrison_Gym.API.Dto;
using Morrison_Gym.API.Entities;
using Morrison_Gym.API.Models.Dto;
using Morrison_Gym.API.Repository.Contract;
using Morrison_Gym.API.Services.TokenService;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Morrison_Gym.API.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly ResponseDto _response;
        private readonly IRepositoryManager _repositoryManager;
        private readonly ITokenService _tokenService;

        public AuthService(IRepositoryManager repositoryManager,
            ITokenService tokenService)
        {
            _repositoryManager = repositoryManager;
            _tokenService = tokenService;
            _response = new ResponseDto();
        }

        public async Task<ResponseDto> Login(Guid code)
        {
            try
            {
                var user = await _repositoryManager.AuthRepository.Login(code);
                if(user is null)
                {
                    _response.Success = false;
                    _response.Message = "User not found";
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user?.Id.ToString()),
                    new Claim(ClaimTypes.Name, user?.UserName),
                    new Claim(ClaimTypes.Role, user?.Role?.Name ?? throw new InvalidOperationException()),
                };

                _response.Result = _tokenService.GenerateAccessToken(claims);
                return _response;
            }

            catch (Exception ex)
            {
                _response.Success = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return _response;
            }
        }

        public async Task<ResponseDto> Register(UserRegisterDto userDto)
        {
            try
            {
                var user = userDto.Adapt<User>();
                user.UserCode = Guid.NewGuid();
                _repositoryManager.AuthRepository.CreateUser(user);
                var result = await _repositoryManager.UnitOfWork.SaveChangesAsync();
                if (result != 0) return _response;
                _response.Success = false;
                _response.Message = "Could not register user";
                return _response;
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return _response;
            }
        }
    }
}
