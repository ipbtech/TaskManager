using Microsoft.AspNetCore.Http.HttpResults;
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
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }


        [HttpPost]
        [Route("api/user/create")]
        public async Task<IActionResult> Create(UserDto user)
        {
            if (ModelState.IsValid)
            {
                bool isCreated = await _userService.Create(user);
                if (isCreated)
                    return Ok();
                else
                    return BadRequest("User with this email already exist");
            }
            return BadRequest("Model is invalid");
        }
    }
}
