using System.ComponentModel.DataAnnotations;

namespace TaskManager.Api.Dto.Validations
{
    public class UserRoleValid : ValidationAttribute
    {
        private Array _roles;
        public UserRoleValid() 
        {
            _roles = Enum.GetValues(typeof(UserRole));
        }

        public override bool IsValid(object? value)
        {
            if (value is UserRole role)
            {
                if (_roles.Cast<UserRole>().Contains(role))
                    return true;
            }
            return false;
        }
    }
}
