using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Helpers;
using TaskManager.Api.Services;
using TaskManager.DTO.Account;
using TaskManager.DTO.User;

namespace TaskManager.Api.Controllers
{
    [Route("api/account/")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UserGetDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var responce = await _userService.Login(loginDto);
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


        [HttpGet("info")]
        [Authorize]
        [ProducesResponseType(typeof(UserGetDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAccountInfo()
        {
            string username = HttpContext.User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
                return Unauthorized();

            var responce = await _userService.Get(username);
            if (responce.IsOkay)
                return Ok(responce.Data);
            else
                return StatusCode(responce.StatusCode, new ErrorResponce
                {
                    Status = responce.StatusCode,
                    ErrorText = responce.Description
                });
        }
    }
}
