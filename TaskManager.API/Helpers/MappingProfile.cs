using AutoMapper;
using System.Text.Json;
using TaskManager.Api.Extensions;
using TaskManager.DAL.Models;
using TaskManager.DTO.Desk;
using TaskManager.DTO.Project;
using TaskManager.DTO.Task;
using TaskManager.DTO.User;

namespace TaskManager.Api.Helpers
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
            CreateMap<User, UserGetDto>();
            CreateMap<User, AdminGetDto>();
            CreateMap<UserCreateDto, User>().ForMember(u => u.HashPassword, opt => opt.MapFrom(u => u.Password.HashSha256()));
            CreateMap<UserUpdateDto, User>();
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
                .ForMember(d => d.DeskColumns, opt => opt.MapFrom(d => DeserializeDeskColumns(d.DeskColumns)));

            CreateMap<DeskCreateDto, Desk>()
                .ForMember(d => d.DeskColumns, opt => opt.MapFrom(d => SerializeEmptyDeskColumns()));
            CreateMap<DeskUpdateDto, Desk>()
                .ForMember(d => d.DeskColumns, opt => opt.UseDestinationValue())
                .ForMember(d => d.CreatedDate, opt => opt.UseDestinationValue());
        }
        private void MapTasks()
        {
            CreateMap<WorkTask, TaskGetShortDto>();
            CreateMap<WorkTask, TaskGetDto>();

            CreateMap<TaskCreateDto, WorkTask>();
            CreateMap<TaskUpdateDto, WorkTask>();

            //dont forget about creatorId
        }


        private byte[] ConvertBase64String(string? src) => src is not null ? Convert.FromBase64String(src) : new byte[] {0};

        private string SerializeEmptyDeskColumns() => JsonSerializer.Serialize(new List<string> { "New" });
        private List<string> DeserializeDeskColumns(string data) => JsonSerializer.Deserialize<List<string>>(data);
    }
}
