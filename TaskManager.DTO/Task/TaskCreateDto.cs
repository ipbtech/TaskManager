using System.ComponentModel.DataAnnotations;

namespace TaskManager.DTO.Task
{
    public class TaskCreateDto : TaskBaseDto
    {
        
        [Required]
        public int DeskId { get; set; }
    }
}
