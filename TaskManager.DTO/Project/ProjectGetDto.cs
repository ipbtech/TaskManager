using System.ComponentModel.DataAnnotations;
using TaskManager.DTO.Desk;
using TaskManager.DTO.Enums;
using TaskManager.DTO.User;

namespace TaskManager.DTO.Project
{
    public class ProjectGetDto : ProjectBaseDto 
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }

        [EnumDataType(typeof(StatusCode))]
        public StatusCode Status { get; set; }
        public AdminGetDto? Admin {  get; set; }

        public List<DeskShortGetDto>? Desks { get; set; }
    }
}
