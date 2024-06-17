using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Dto;
using TaskManager.Api.Services;

namespace TaskManager.Api.Controllers
{
    [Route("api/user/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }


        [HttpGet("get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var responce = await _userService.Get(id);
            if (responce.IsOkay)
                return Ok(responce.Data);
            else
                return NotFound("User not found");
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            var responce = await _userService.GetAll();
            if (responce.IsOkay)
                return Ok(responce.Data);
            else
                return BadRequest();
        }



        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] UserDto user)
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

        [HttpPost("create/multiple")]
        public async Task<IActionResult> CreateMultiple([FromBody] List<UserDto> userDtos)
        {
            if (ModelState.IsValid)
            {
                bool isCreated = await _userService.CreateMutiple(userDtos);
                if (isCreated)
                    return Ok();
                else
                    return BadRequest("User with this email already exist");
            }
            return BadRequest("Model is invalid");
        }

        [HttpPatch("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserDto user)
        {
            if (ModelState.IsValid)
            {
                var responce = await _userService.Update(id, user);
                if (responce.IsOkay)
                    return Ok(responce.Data);
                else
                    return NotFound("User not found");
               
            }
            return BadRequest("Model is invalid");
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool isDel = await _userService.Delete(id);
            if (isDel)
                return Ok();
            else
                return NotFound("User not found");
        }
    }
}
