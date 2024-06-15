using TaskManager.Api.Dto;
using TaskManager.Api.Services.Helpers;
using TaskManager.Domain;

namespace TaskManager.Api.Services
{
    public static class DtosExtension
    {
        public static User FromDto(this UserDto dto)
        {
            return new User()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Password = dto.Password.HashSha256(),
                Phone = dto.Phone,
                Role = (Domain.UserRole)dto.Role
            };
        }

        public static UserDto ToDto(this User model)
        {
            return new UserDto();
        }
    }
}
