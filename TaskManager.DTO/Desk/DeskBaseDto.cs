using System.ComponentModel.DataAnnotations;

namespace TaskManager.DTO.Desk
{
    public abstract class DeskBaseDto
    {
        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }
    }
}
