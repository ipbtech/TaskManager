using System.ComponentModel.DataAnnotations;

namespace TaskManager.DTO.Desk
{
    public class DeskCreateDto : DeskBaseDto
    {
        [Required]
        public int ProjectId { get; set; }
    }
}
