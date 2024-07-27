using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TaskManager.API.Helpers;
using TaskManager.API.Services;
using TaskManager.DTO.Enums;
using TaskManager.DTO.Project;
using TaskManager.DTO.User;

namespace TaskManager.API.Controllers
{
    [Route("api/project")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectService _projectService;
        private readonly UserService _userService;

        public ProjectController(ProjectService projectService, UserService userService)
        {
            _projectService = projectService;
            _userService = userService;
        }


        [HttpPost("create")]
        [Authorize(Roles = "SystemOwner")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] ProjectCreateDto createDto)
        {
            if (ModelState.IsValid)
            {
                var responce =  await _projectService.Create(createDto);
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


        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "SystemOwner")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            var responce = await _projectService.Delete(id);
            if (responce.IsOkay)
                return Ok();
            else
                return StatusCode(responce.StatusCode, new ErrorResponce
                {
                    Status = responce.StatusCode,
                    ErrorText = responce.Description
                });
        }


        [HttpPatch("update/{id}")]
        [Authorize(Roles = "SystemOwner,Admin")]
        [ProducesResponseType(typeof(ProjectGetDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] ProjectUpdateDto project)
        {
            if (ModelState.IsValid)
            {
                var responce = await _projectService.Update(id, project);
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



        [HttpGet("get/{id}")]
        [Authorize]
        [ProducesResponseType(typeof(ProjectGetDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            var responce = await _projectService.Get(id);
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
        [ProducesResponseType(typeof(IEnumerable<ProjectGetDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            string username = HttpContext.User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
                return Unauthorized();

            BaseResponce<IEnumerable<ProjectBaseDto>> responce;
            var userResponce = await _userService.Get(username);
            if (userResponce.IsOkay && userResponce.Data is UserGetDto user)
            {
                if (user.Role == UserRole.SystemOwner)
                    responce = await _projectService.GetAll();
                else
                    responce = await _projectService.GetByUserId(user.Id);

                if (responce.IsOkay)
                    return Ok(responce.Data);
                else
                    return StatusCode(responce.StatusCode, new ErrorResponce
                    {
                        Status = responce.StatusCode,
                        ErrorText = responce.Description
                    });
            }
            else
                return StatusCode(userResponce.StatusCode, new ErrorResponce
                {
                    Status = userResponce.StatusCode,
                    ErrorText = userResponce.Description
                });
        }


        [HttpPatch("{projectId}/add-users")]
        [Authorize(Roles = "SystemOwner,Admin")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddUsersToProject(int projectId, [FromBody] List<int> userIds)
        {
            if (ModelState.IsValid)
            {
                var responce = await _projectService.AddUsersToProject(projectId, userIds);
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

        [HttpPatch("{projectId}/remove-users")]
        [Authorize(Roles = "SystemOwner,Admin")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveUsersFromProject(int projectId, [FromBody] List<int> userIds)
        {
            if (ModelState.IsValid)
            {
                var responce = await _projectService.RemoveUsersFromProject(projectId, userIds);
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
    }
}
