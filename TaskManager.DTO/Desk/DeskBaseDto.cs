using System.ComponentModel.DataAnnotations;

namespace TaskManager.DTO.Desk
{
    public abstract class DeskBaseDto
    {
        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string? Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Base64String]
        public byte[]? Image { get; set; }
    }
}
