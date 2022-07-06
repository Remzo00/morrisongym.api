using AutoMapper;
using Morrison_Gym.API.Models;
using Morrison_Gym.API.Models.Dto;

namespace Morrison_Gym.API
{
    public class Maps : Profile
    {
        public Maps()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Coach, CoachDto>().ReverseMap();
            CreateMap<Customer, CustomerDto>().ReverseMap();
        }
    }
}
