using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TaskManager.API.Extensions;
using TaskManager.API.Helpers;
using TaskManager.Dal.Repository;
using TaskManager.DAL.Models;
using TaskManager.DTO.Account;
using TaskManager.DTO.User;

namespace TaskManager.API.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepo;
        private readonly IRepository<Project> _projectRepo;
        private readonly IMapper _mapper;

        public UserService(IRepository<User> userRepo, IRepository<Project> projectRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _projectRepo = projectRepo;
            _mapper = mapper;
        }

        public async Task<BaseResponce<bool>> Create(UserCreateDto entity)
        {
            try
            {
                if (!_userRepo.GetAll().AsNoTracking().Any(user => user.Email == entity.Email))
                {
                    var user = _mapper.Map<User>(entity);
                    await _userRepo.Create(user);
                    return new BaseResponce<bool>
                    {
                        IsOkay = true,
                        Data = true
                    };
                }
                return new BaseResponce<bool>
                {
                    IsOkay = false,
                    StatusCode = 400,
                    Description = "User with passed email already exists"
                };
            }
            catch (Exception ex)
            {
                //TODO logging
                return new BaseResponce<bool>
                {
                    IsOkay = false,
                    StatusCode = 500,
                    Description = "Internal server error"
                };
            }
        }

        public async Task<BaseResponce<bool>> CreateMultiple(IEnumerable<UserCreateDto> userDtos)
        {
            if (userDtos.Any())
            {
                foreach (var userDto in userDtos)
                {
                    var createAction = await Create(userDto);
                    if (!createAction.IsOkay)
                        return createAction;
                }
                return new BaseResponce<bool>
                {
                    IsOkay = true,
                    Data = true
                };
            }
            return new BaseResponce<bool>
            {
                IsOkay = false,
                StatusCode = 400,
                Description = "Users collection is empty"
            };
        }

        public async Task<BaseResponce<bool>> Delete(int id)
        {
            try
            {
                var user = _userRepo.GetAll().FirstOrDefault(user => user.Id == id);
                if (user is not null)
                {
                    await _userRepo.Delete(user);
                    return new BaseResponce<bool>
                    {
                        IsOkay = true,
                        Data = true
                    };
                }
                return new BaseResponce<bool>
                {
                    IsOkay = false,
                    StatusCode = 404,
                    Description = "User not found"
                };
            }
            catch (Exception ex)
            {
                //TODO logging
                return new BaseResponce<bool>
                {
                    IsOkay = false,
                    StatusCode = 500,
                    Description = "Internal server error"
                };
            }
        }

        public async Task<BaseResponce<UserBaseDto>> Update(int id, UserUpdateDto entity)
        {
            try
            {
                var user = await _userRepo.GetAll().FirstOrDefaultAsync(user => user.Id == id);
                if (user is not null)
                {
                    _mapper.Map(entity, user);
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
                    StatusCode = 404,
                    Description = "User not found"
                };
            }
            catch (Exception ex)
            {
                //TODO logging
                return new BaseResponce<UserBaseDto>
                {
                    IsOkay = false,
                    StatusCode = 500,
                    Description = "Internal Server Error"
                };
            }
        }
     
        public async Task<BaseResponce<UserBaseDto>> Get(int id)
        {
            try
            {
                var user = await _userRepo.GetAll().AsNoTracking().FirstOrDefaultAsync(user => user.Id == id);
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
                    StatusCode = 404,
                    Description = "User not found"
                };
            }
            catch (Exception ex)
            {
                //TODO logging
                return new BaseResponce<UserBaseDto>
                {
                    IsOkay = false,
                    StatusCode = 500,
                    Description = "Internal Server Error"
                };
            }
        }

        public async Task<BaseResponce<UserBaseDto>> Get(string email)
        {
            try
            {
                var user = await _userRepo.GetAll().AsNoTracking().FirstOrDefaultAsync(user => user.Email == email);
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
                    StatusCode = 404,
                    Description = "User not found"
                };
            }
            catch (Exception ex)
            {
                //TODO logging
                return new BaseResponce<UserBaseDto>
                {
                    IsOkay = false,
                    StatusCode = 500,
                    Description = "Internal Server Error"
                };
            }
        }

        public async Task<BaseResponce<IEnumerable<UserBaseDto>>> GetAll()
        {
            try
            {
                var users = await _userRepo.GetAll().AsNoTracking().ToListAsync();
                return new BaseResponce<IEnumerable<UserBaseDto>>
                {
                    IsOkay = true,
                    Data = _mapper.Map<IEnumerable<UserGetDto>>(users)
                };
            }
            catch (Exception ex)
            {
                //TODO logging
                return new BaseResponce<IEnumerable<UserBaseDto>>
                {
                    IsOkay = true,
                    StatusCode = 500,
                    Description = "Internal server error"
                };
            }
        }

        public async Task<BaseResponce<IEnumerable<UserBaseDto>>> GetAllByRole(DTO.Enums.UserRole role)
        {
            try
            {
                var users = await _userRepo.GetAll().AsNoTracking()
                    .Where(user => user.Role == (UserRole)role).ToListAsync();
                return new BaseResponce<IEnumerable<UserBaseDto>>
                {
                    IsOkay = true,
                    Data = _mapper.Map<IEnumerable<UserGetDto>>(users)
                };
            }
            catch (Exception ex)
            {
                //TODO logging
                return new BaseResponce<IEnumerable<UserBaseDto>>
                {
                    IsOkay = true,
                    StatusCode = 500,
                    Description = "Internal server error"
                };
            }
        }

        public async Task<BaseResponce<IEnumerable<UserBaseDto>>> GetByProject(int projectId)
        {
            try
            {
                var project = await _projectRepo.GetAll().AsNoTracking()
                    .Include(p => p.ProjectUsers).FirstOrDefaultAsync(p => p.Id == projectId);
                if (project is not null)
                {
                    var users = project.ProjectUsers;
                    return new BaseResponce<IEnumerable<UserBaseDto>>
                    {
                        IsOkay = true,
                        Data = _mapper.Map<IEnumerable<UserGetDto>>(users)
                    };
                }
                return new BaseResponce<IEnumerable<UserBaseDto>>
                {
                    IsOkay = false,
                    StatusCode = 404,
                    Description = "Project not found"
                };
            }
            catch (Exception ex)
            {
                //TODO logging
                return new BaseResponce<IEnumerable<UserBaseDto>>
                {
                    IsOkay = true,
                    StatusCode = 500,
                    Description = "Internal server error"
                };
            }
        }

        public async Task<BaseResponce<SignInResultDto>> Login(LoginDto loginDto)
        {
            try
            {
                var user = await _userRepo.GetAll()
                    .FirstOrDefaultAsync(user => user.Email == loginDto.Email && user.HashPassword == loginDto.Password.HashSha256());
                if (user is null)
                {
                    return new BaseResponce<SignInResultDto>()
                    {
                        IsOkay = false,
                        StatusCode = 400,
                        Description = "Uncorrect email or password"
                    };
                }

                user.LastLoginDate = DateTime.Now;
                await _userRepo.Update(user);

                var token = CreateJwtToken(user);
                return new BaseResponce<SignInResultDto>()
                {
                    IsOkay = true,
                    Data = new SignInResultDto()
                    {
                        User = _mapper.Map<UserGetDto>(user),
                        AccessToken = token
                    }
                };
            }
            catch (Exception ex)
            {
                //TODO logging
                return new BaseResponce<SignInResultDto>
                {
                    IsOkay = false,
                    StatusCode = 500,
                    Description = "Internal server error"
                };
            }
        }


        private static string CreateJwtToken(User user)
        {
            var now = DateTime.UtcNow;
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
            };

            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: claims,
                    expires: now.Add(TimeSpan.FromDays(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }
    }
}
