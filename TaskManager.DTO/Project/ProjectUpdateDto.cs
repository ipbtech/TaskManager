using System.ComponentModel.DataAnnotations;
using TaskManager.DTO.Enums;

namespace TaskManager.DTO.Project
{
    public class ProjectUpdateDto : ProjectCreateDto
    {
        [EnumDataType(typeof(StatusCode))]
        public StatusCode Status { get; set; }
    }
}
