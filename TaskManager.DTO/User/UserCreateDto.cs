using System.ComponentModel.DataAnnotations;
using TaskManager.DTO.Enums;

namespace TaskManager.DTO.User
{
    public class UserCreateDto : UserBaseDto
    {
        [Required]
        [MaxLength(100)]
        public string Password { get; set; }
    }
}
