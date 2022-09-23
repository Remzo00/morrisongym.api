using AutoMapper;
using Morrison_Gym.API.Dto;
using Morrison_Gym.API.Entities;
using Morrison_Gym.API.Models.Dto;

namespace Morrison_Gym.API
{
    public class Maps : Profile
    {
        public Maps()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Coach, CoachDto>().ReverseMap();
            CreateMap<Coach, CoachCreateDto>().ReverseMap();
            CreateMap<Coach, CoachUpdateDto>().ReverseMap();
            CreateMap<Customer, CustomerDto>().ReverseMap();
        }
    }
}
