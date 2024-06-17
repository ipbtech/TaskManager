//using TaskManager.Api.Dto;
//using TaskManager.Domain;

//namespace TaskManager.Api.Services.Helpers
//{
//    public static class DtosExtension
//    {
//        public static User FromDto(this UserDto dto)
//        {
//            return new User()
//            {
//                FirstName = dto.FirstName,
//                LastName = dto.LastName,
//                Email = dto.Email,
//                Password = dto.Password,
//                HashPassword = dto.Password.HashSha256(),
//                Phone = dto.Phone,
//                Role = (Domain.UserRole)dto.Role
//            };
//        }

//        public static UserDto ToDto(this User model)
//        {
//            return new UserDto()
//            {
//                Id = model.Id,
//                FirstName = model.FirstName,
//                LastName = model.LastName,
//                Email = model.Email,
//                Password = null,
//                Phone = model.Phone,
//                Role = (Dto.UserRole)model.Role
//            };
//        }
//    }
//}
