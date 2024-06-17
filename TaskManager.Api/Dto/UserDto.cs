using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TaskManager.Api.Dto
{
    public class UserDto
    {
        [SwaggerIgnore]
        public int Id { get; set; }
        
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

        [Required]
        [MaxLength(100)]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Password { get; set; }

        [Phone]
        [MaxLength(15)]
        public string? Phone { get; set; }

        [EnumDataType(typeof(UserRole))]
        public UserRole? Role { get; set; }
    }
}
