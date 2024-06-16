using System.ComponentModel.DataAnnotations;
using TaskManager.Api.Dto.Validations;

namespace TaskManager.Api.Dto
{
    public class UserDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Phone]
        public string? Phone { get; set; }

        [UserRoleValid]
        public UserRole? Role { get; set; }
    }
}
