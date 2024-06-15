using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Dto;
using TaskManager.Api.Services;
using TaskManager.Dal.Repository;
using TaskManager.Domain;

namespace TaskManager.Api.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepository<User> _usersRepo;

        public UserController(IRepository<User> repository)
        {
            _usersRepo = repository;
        }

        [HttpGet]
        [Route("api/user/test")]
        public IActionResult Test()
        {
            return Ok("Hello world!");
        }

        [HttpPost]
        [Route("api/user/create")]
        public async Task<IActionResult> Create(UserDto user)
        {
            if (ModelState.IsValid)
            {
                await _usersRepo.Create(user.FromDto());
                return Ok();
            }
            return BadRequest();
        }
    }
}
