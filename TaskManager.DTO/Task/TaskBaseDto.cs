using System.ComponentModel.DataAnnotations;

namespace TaskManager.DTO.Task
{
    public abstract class TaskBaseDto
    {
        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }

        [Required]
        public string? ColumnOfDesk { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        public int? ContractorId { get; set; }


        [Base64String]
        public string? ImageAsBase64 { get; set; }
    }
}
