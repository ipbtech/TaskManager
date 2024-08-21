using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Helpers;
using TaskManager.Api.Services;
using TaskManager.DTO.Enums;
using TaskManager.DTO.User;

namespace TaskManager.Api.Controllers
{
    [Route("api/user/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }



        [HttpGet("get/{id}")]
        [Authorize]
        [ProducesResponseType(typeof(UserGetDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Get(int id)
        {
            var responce = await _userService.Get(id);
            if (responce.IsOkay)
                return Ok(responce.Data);
            else
                return StatusCode(responce.StatusCode, new ErrorResponce
                {
                    Status = responce.StatusCode,
                    ErrorText = responce.Description
                });
        }


        [HttpGet("get/all")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<UserGetDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Get()
        {
            var responce = await _userService.GetAll();
            if (responce.IsOkay)
                return Ok(responce.Data);
            else
                return StatusCode(responce.StatusCode, new ErrorResponce
                {
                    Status = responce.StatusCode,
                    ErrorText = responce.Description
                });
        }

        [HttpGet("get/admins")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<UserGetDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAdminUsers()
        {
            var responce = await _userService.GetAllByRole(UserRole.Admin);
            if (responce.IsOkay)
                return Ok(responce.Data);
            else
                return StatusCode(responce.StatusCode, new ErrorResponce
                {
                    Status = responce.StatusCode,
                    ErrorText = responce.Description
                });
        }

        [HttpGet("get/by-project/{projectId}")]
        [Authorize(Roles = "SystemOwner,Admin,Editor")]
        [ProducesResponseType(typeof(IEnumerable<UserGetDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUsersByProjectId(int projectId)
        {
            var responce = await _userService.GetByProject(projectId);
            if (responce.IsOkay)
                return Ok(responce.Data);
            else
                return StatusCode(responce.StatusCode, new ErrorResponce
                {
                    Status = responce.StatusCode,
                    ErrorText = responce.Description
                });
        }


        [HttpPost("create")]
        [Authorize(Roles = "SystemOwner")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] UserCreateDto user)
        {
            if (ModelState.IsValid)
            {
                var responce = await _userService.Create(user);
                if (responce.IsOkay) 
                    return Ok();
                else
                    return StatusCode(responce.StatusCode, new ErrorResponce 
                    { 
                        Status = responce.StatusCode,
                        ErrorText = responce.Description 
                    });
            }
            return BadRequest(new ErrorResponce { Status = 400, ErrorText = "Passed model is invalid" });
        }


        [HttpPost("create/multiple")]
        [Authorize(Roles = "SystemOwner")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateMultiple([FromBody] List<UserCreateDto> userDtos)
        {
            if (ModelState.IsValid && userDtos.Any())
            {
                var responce = await _userService.CreateMultiple(userDtos);
                if (responce.IsOkay)
                    return Ok();
                else
                    return StatusCode(responce.StatusCode, new ErrorResponce
                    {
                        Status = responce.StatusCode,
                        ErrorText = responce.Description
                    });
            }
            return BadRequest(new ErrorResponce { Status = 400, ErrorText = "Passed model is invalid" });
        }


        [HttpPut("update/{id}")]
        [Authorize(Roles = "SystemOwner")]
        [ProducesResponseType(typeof(UserGetDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] UserUpdateDto user)
        {
            if (ModelState.IsValid)
            {
                var responce = await _userService.Update(id, user);
                if (responce.IsOkay)
                    return Ok(responce.Data);
                else
                    return StatusCode(responce.StatusCode, new ErrorResponce
                    {
                        Status = responce.StatusCode,
                        ErrorText = responce.Description
                    });
            }
            return BadRequest(new ErrorResponce { Status = 400, ErrorText = "Passed model is invalid" });
        }


        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "SystemOwner")]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            var responce = await _userService.Delete(id);
            if (responce.IsOkay)
                return Ok();
            else
                return StatusCode(responce.StatusCode, new ErrorResponce
                {
                    Status = responce.StatusCode,
                    ErrorText = responce.Description
                });
        }
    }
}
