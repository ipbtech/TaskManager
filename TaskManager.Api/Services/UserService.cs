using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Dto;
using TaskManager.Api.Dto.Base;
using TaskManager.Api.Services.Helpers;
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

        public async Task<bool> Delete(int id)
        {
            var user = _userRepo.GetAll().FirstOrDefault(user => user.Id == id);
            if (user is not null)
            {
                await _userRepo.Delete(user);
                return true;
            }
            return false;
        }

        public async Task<BaseResponce<UserDto>> Update(int id, UserDto entity)
        {
            var user = _userRepo.GetAll().FirstOrDefault(user => user.Id == id);
            if (user is not null)
            {
                user.FirstName = entity.FirstName;
                user.LastName = entity.LastName;
                user.Email = entity.Email;
                user.Password = entity.Password;
                user.HashPassword = entity.Password.HashSha256();
                user.Phone = entity.Phone;
                user.Role = (Domain.UserRole)entity.Role;

                User newUser = await _userRepo.Update(user);
                return new BaseResponce<UserDto>
                {
                    IsOkay = true,
                    Data = newUser.ToDto(),
                };
            }
            return new BaseResponce<UserDto>
            {
                IsOkay = false,
                Description = "Model doesn't exist"
            };
        }
     
        public async Task<BaseResponce<UserDto>> Get(int id)
        {
            var user = await _userRepo.GetAll().FirstOrDefaultAsync(user => user.Id == id);
            if (user is not null)
            {
                return new BaseResponce<UserDto>
                {
                    IsOkay = true,
                    Data = user.ToDto()
                };
            }
            return new BaseResponce<UserDto>
            {
                IsOkay = false,
                Description = "Model doesn't exist"
            };
        }

        public async Task<BaseResponce<List<UserDto>>> GetAll()
        {
            var users = await _userRepo.GetAll().ToListAsync();
            return new BaseResponce<List<UserDto>>
            {
                IsOkay = true,
                Data = users.Select(user => user.ToDto()).ToList()
            };
        }


        public async Task<bool> CreateMutiple(List<UserDto> userDtos)
        {
            if (userDtos.Any()) 
            {
                foreach(var userDto in userDtos)
                {
                    
                    bool isCreated = await Create(userDto);
                    if (!isCreated)
                        return false;
                }
                return true;
            }
            return false;
        }
    }
}
