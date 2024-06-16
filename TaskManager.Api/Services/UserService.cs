using TaskManager.Api.Dto;
using TaskManager.Dal.Repository;
using TaskManager.Domain;

namespace TaskManager.Api.Services
{
    public class UserService : IService<UserDto>
    {
        private readonly IRepository<User> _userRepo;

        public UserService(IRepository<User> userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<bool> Create(UserDto entity)
        {
            if (!_userRepo.GetAll().Any(user => user.Email == entity.Email))
            {
                var model = entity.FromDto();
                await _userRepo.Create(model);
                return true;
            }
            return false;
        }
    }
}
