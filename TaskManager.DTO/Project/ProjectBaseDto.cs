using System.ComponentModel.DataAnnotations;

namespace TaskManager.DTO.Project
{
    public abstract class ProjectBaseDto
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
        public string? ImageAsBase64 { get; set; }
    }
}
