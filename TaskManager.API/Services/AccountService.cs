using AutoMapper;
using TaskManager.DAL.Models;
using TaskManager.Dal.Repository;
using TaskManager.DTO.User;
using TaskManager.DTO.Account;
using Microsoft.EntityFrameworkCore;
using TaskManager.API.Extensions;
using TaskManager.API.Helpers;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace TaskManager.API.Services
{
    public class AccountService
    {
        private readonly IRepository<User> _userRepo;
        private readonly IMapper _mapper;

        public AccountService(IRepository<User> userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<BaseResponce<SignInResultDto>> Login(LoginDto loginDto)
        {
            try
            {
                var user = await _userRepo.GetAll().FirstOrDefaultAsync(user => user.Email == loginDto.Email &&
                                                                    user.HashPassword == loginDto.Password.HashSha256());
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
