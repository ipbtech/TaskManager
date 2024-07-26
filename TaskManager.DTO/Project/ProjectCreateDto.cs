using System.ComponentModel.DataAnnotations;

namespace TaskManager.DTO.Project
{
    public class ProjectCreateDto : ProjectBaseDto 
    {
        [Required]
        public int AdminId { get; set; }
    }
}
