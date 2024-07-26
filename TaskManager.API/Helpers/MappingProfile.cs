using AutoMapper;
using System.ComponentModel.DataAnnotations;
using TaskManager.API.Extensions;
using TaskManager.DAL.Models;
using TaskManager.DTO.Desk;
using TaskManager.DTO.Project;
using TaskManager.DTO.User;

namespace TaskManager.API.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            MapUsers();
            MapProjects();
            MapDesks();
            MapTasks();
        }

        
        private void MapUsers()
        {
            CreateMap<UserUpdateDto, User>();
            CreateMap<User, UserGetDto>();
            CreateMap<UserCreateDto, User>().ForMember(u => u.HashPassword, opt => opt.MapFrom(u => u.Password.HashSha256()));
        }
        private void MapProjects()
        {
            CreateMap<ProjectCreateDto, Project>()
                .ForMember(p => p.Image, opt => opt.MapFrom(p => ConvertBase64String(p.ImageAsBase64)));

            CreateMap<ProjectUpdateDto, Project>()
                .ForMember(p => p.Image, opt => opt.MapFrom(p => ConvertBase64String(p.ImageAsBase64)))
                .ForMember(p => p.CreatedDate, opt => opt.UseDestinationValue());

            CreateMap<Project, ProjectGetDto>()
                .ForMember(p => p.ImageAsBase64, opt => opt.MapFrom(p => Convert.ToBase64String(p.Image ?? new byte[] { 0 })));
        }
        private void MapDesks()
        {
            CreateMap<Desk, DeskShortGetDto>();
        }
        private void MapTasks()
        {
            
        }


        private byte[] ConvertBase64String(string? src) => src is not null ? Convert.FromBase64String(src) : new byte[] {0};
    }
}
