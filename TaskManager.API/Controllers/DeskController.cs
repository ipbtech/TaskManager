using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Helpers;
using TaskManager.Api.Services;
using TaskManager.DTO.Desk;
using TaskManager.DTO.Desk.ColumnDesk;

namespace TaskManager.Api.Controllers
{
    [Route("api/desk")]
    [ApiController]
    public class DeskController : ControllerBase
    {
        private readonly IDeskService _deskService;

        public DeskController(IDeskService deskService)
        {
            _deskService = deskService;
        }



        [HttpPost("create")]
        [Authorize(Roles = "SystemOwner,Admin")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] DeskCreateDto createDto)
        {
            string? username = HttpContext.User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
                return Unauthorized();

            if (ModelState.IsValid)
            {
                var responce = await _deskService.Create(createDto, username);
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
        [Authorize(Roles = "SystemOwner,Admin")]
        [ProducesResponseType(typeof(DeskGetDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] DeskUpdateDto updateDto)
        {
            string? username = HttpContext.User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
                return Unauthorized();

            if (ModelState.IsValid)
            {
                var responce = await _deskService.Update(id, updateDto, username);
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
        [Authorize(Roles = "SystemOwner,Admin")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            string? username = HttpContext.User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
                return Unauthorized();

            var responce = await _deskService.Delete(id, username);
            if (responce.IsOkay)
                return Ok();
            else
                return StatusCode(responce.StatusCode, new ErrorResponce
                {
                    Status = responce.StatusCode,
                    ErrorText = responce.Description
                });
        }


        [HttpGet("get/{id}")]
        [Authorize]
        [ProducesResponseType(typeof(DeskGetDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            var responce = await _deskService.Get(id);
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
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<DeskGetDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByProject(int projectId)
        {
            var responce = await _deskService.GetByProjectId(projectId);
            if (responce.IsOkay)
                return Ok(responce.Data);
            else
                return StatusCode(responce.StatusCode, new ErrorResponce
                {
                    Status = responce.StatusCode,
                    ErrorText = responce.Description
                });
        }


        [HttpPatch("columns/add")]
        [Authorize(Roles = "SystemOwner,Admin")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddColumnDesk([FromBody] AddingColumnDto columnDto)
        {
            string? username = HttpContext.User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
                return Unauthorized();

            var responce = await _deskService.AddColumnDesk(columnDto, username);
            if (responce.IsOkay)
                return Ok(responce.Data);
            else
                return StatusCode(responce.StatusCode, new ErrorResponce
                {
                    Status = responce.StatusCode,
                    ErrorText = responce.Description
                });
        }

        [HttpPatch("columns/update")]
        [Authorize(Roles = "SystemOwner,Admin")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateColumnDesk([FromBody] UpdatingColumnDto columnDto)
        {
            string? username = HttpContext.User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
                return Unauthorized();

            var responce = await _deskService.UpdateColumnDesk(columnDto, username);
            if (responce.IsOkay)
                return Ok(responce.Data);
            else
                return StatusCode(responce.StatusCode, new ErrorResponce
                {
                    Status = responce.StatusCode,
                    ErrorText = responce.Description
                });
        }


        [HttpPatch("columns/delete")]
        [Authorize(Roles = "SystemOwner,Admin")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteColumnDesk([FromBody] DeletingColumnDto columnDto)
        {
            string? username = HttpContext.User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
                return Unauthorized();

            var responce = await _deskService.DeleteColumnDesk(columnDto, username);
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
