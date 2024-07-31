using AutoMapper;
using System.Text.Json;
using TaskManager.API.Extensions;
using TaskManager.DAL.Models;
using TaskManager.DTO.Desk;
using TaskManager.DTO.Project;
using TaskManager.DTO.Task;
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
            CreateMap<User, AdminGetDto>();
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

            CreateMap<Project, ProjectGetShortDto>();
        }
        private void MapDesks()
        {
            CreateMap<Desk, DeskGetShortDto>();
            CreateMap<Desk, DeskGetDto>()
                .ForMember(d => d.DeskColumns, opt => opt.MapFrom(d => Deserialize(d.DeskColumns)));

            CreateMap<DeskCreateDto, Desk>()
                .ForMember(d => d.DeskColumns, opt => opt.MapFrom(d => Serialize(d.DeskColumns)));
            CreateMap<DeskUpdateDto, Desk>()
                .ForMember(d => d.DeskColumns, opt => opt.MapFrom(d => Serialize(d.DeskColumns)));
        }
        private void MapTasks()
        {
            CreateMap<Task, TaskGetShortDto>();
        }


        private byte[] ConvertBase64String(string? src) => src is not null ? Convert.FromBase64String(src) : new byte[] {0};

        private string Serialize(List<string>? data)
        {
            if (data is not null && data.Any())
                return JsonSerializer.Serialize(data);
            return JsonSerializer.Serialize(new List<string> { "New" });
        }
        private List<string> Deserialize(string data) => JsonSerializer.Deserialize<List<string>>(data);
    }
}
