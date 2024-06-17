using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManager.API.Services.Base;
using TaskManager.Dal.Repository;
using TaskManager.DAL.Models;
using TaskManager.DTO.User;

namespace TaskManager.Api.Services
{
    public class UserService : IService<UserBaseDto>
    {
        private readonly IRepository<User> _userRepo;
        private readonly IMapper _mapper;

        public UserService(IRepository<User> userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<bool> Create(UserBaseDto entity)
        {
            if (!_userRepo.GetAll().Any(user => user.Email == entity.Email))
            {
                var model = _mapper.Map<User>(entity);
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

        public async Task<BaseResponce<UserBaseDto>> Update(int id, UserBaseDto entity)
        {
            var user = _userRepo.GetAll().FirstOrDefault(user => user.Id == id);
            if (user is not null)
            {
                user.FirstName = entity.FirstName;
                user.LastName = entity.LastName;
                user.Email = entity.Email;
                user.Phone = entity.Phone;
                user.Role = (UserRole)entity.Role;

                User newUser = await _userRepo.Update(user);
                return new BaseResponce<UserBaseDto>
                {
                    IsOkay = true,
                    Data = _mapper.Map<UserUpdateDto>(newUser)
                };
            }
            return new BaseResponce<UserBaseDto>
            {
                IsOkay = false,
                Description = "Model doesn't exist"
            };
        }
     
        public async Task<BaseResponce<UserBaseDto>> Get(int id)
        {
            var user = await _userRepo.GetAll().FirstOrDefaultAsync(user => user.Id == id);
            if (user is not null)
            {
                return new BaseResponce<UserBaseDto>
                {
                    IsOkay = true,
                    Data = _mapper.Map<UserGetDto>(user)
                };
            }
            return new BaseResponce<UserBaseDto>
            {
                IsOkay = false,
                Description = "Model doesn't exist"
            };
        }

        public async Task<BaseResponce<IEnumerable<UserBaseDto>>> GetAll()
        {
            var users = await _userRepo.GetAll().ToListAsync();
            return new BaseResponce<IEnumerable<UserBaseDto>>
            {
                IsOkay = true,
                Data = _mapper.Map<IEnumerable<UserGetDto>>(users)
            };
        }


        public async Task<bool> CreateMutiple(IEnumerable<UserBaseDto> userDtos)
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
