using AutoMapper;
using TaskManager.API.Extensions;
using TaskManager.DAL.Models;
using TaskManager.DTO.User;

namespace TaskManager.API.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserCreateDto>().ReverseMap()
                .ForMember(u => u.HashPassword, opt => opt.MapFrom(u => u.Password.HashSha256()));
            CreateMap<User, UserUpdateDto>().ReverseMap();
            CreateMap<User, UserGetDto>().ReverseMap();
        }
    }
}
