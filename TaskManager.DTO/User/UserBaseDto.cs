using System.ComponentModel.DataAnnotations;
using TaskManager.DTO.Enums;

namespace TaskManager.DTO.User
{
    public abstract class UserBaseDto
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [Phone]
        [MaxLength(15)]
        public string? Phone { get; set; }

        [EnumDataType(typeof(UserRole))]
        public UserRole? Role { get; set; }
    }
}
