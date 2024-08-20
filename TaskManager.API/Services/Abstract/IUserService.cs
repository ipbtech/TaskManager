using TaskManager.Api.Helpers;
using TaskManager.DTO.Account;
using TaskManager.DTO.Enums;
using TaskManager.DTO.User;

namespace TaskManager.Api.Services
{
    public interface IUserService
    {
        public Task<BaseResponce<IEnumerable<UserBaseDto>>> GetAll();
        public Task<BaseResponce<IEnumerable<UserBaseDto>>> GetAllByRole(UserRole role);
        public Task<BaseResponce<IEnumerable<UserBaseDto>>> GetByProject(int projectId);
        public Task<BaseResponce<UserBaseDto>> Get(int id);
        public Task<BaseResponce<UserBaseDto>> Get(string email);
        public Task<BaseResponce<bool>> Create(UserCreateDto entity);
        public Task<BaseResponce<bool>> CreateMultiple(IEnumerable<UserCreateDto> userDtos);
        public Task<BaseResponce<bool>> Delete(int id);
        public Task<BaseResponce<UserBaseDto>> Update(int id, UserUpdateDto entity);
        public Task<BaseResponce<SignInResultDto>> Login(LoginDto loginDto);
    }
}