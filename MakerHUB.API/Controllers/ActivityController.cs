using MakerHUB.API.Extensions;
using MakerHUB.BLL.DTO;
using MakerHUB.BLL.Exceptions;
using MakerHUB.BLL.Services.ActivityServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MakerHUB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly ActivityService _activityService;

        public ActivityController(ActivityService activityService)
        {
            _activityService = activityService;
        }

        [HttpGet("{activityId}")]
        [Authorize]
        public ActionResult<ActivityDTO> Get(int activityId)
        {
            try
            {
                ActivityDTO? activityDTO = _activityService.GetById(activityId, this.GetUserId());
                return Ok(activityDTO);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("GetAllByUser")]
        [Authorize]
        public ActionResult<IEnumerable<ActivityDTO>> GetAllByUser()
        {
            IEnumerable<ActivityDTO> activityDTOs = _activityService.GetListByUserId(this.GetUserId());

            return Ok(activityDTOs);
        }

        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<ActivityDTO>> GetAll()
        {
            IEnumerable<ActivityDTO> activityDTOs = _activityService.GetListPublic(this.GetUserId());

            return Ok(activityDTOs);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add([FromBody] ActivityAddDTO activityAddDTO)
        {
            try
            {
                int activityId = _activityService.Create(activityAddDTO, this.GetUserId());
                return Ok(activityId);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Authorize]
        public IActionResult Modify(ActivityEditDTO activityEditDTO)
        {
            try
            {
                _activityService.Update(activityEditDTO, this.GetUserId());
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ForbiddenException ex)
            {
                return StatusCode(403, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{activityId}")]
        [Authorize]
        public IActionResult Delete(int activityId)
        {
            try
            {
                _activityService.Delete(activityId, this.GetUserId());
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ForbiddenException ex)
            {
                return StatusCode(403, ex.Message);
            }
        }

        [HttpPost("activity-like")]
        [Authorize]
        public IActionResult Like([FromBody] ActivityIdDTO activityIdDTO)
        {
            try
            {
                _activityService.Like(activityIdDTO.ActivityId, this.GetUserId());
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (AlreadyExistException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("activity-dislike")]
        [Authorize]
        public IActionResult Dislike([FromBody] ActivityIdDTO activityIdDTO)
        {
            try
            {
                _activityService.Dislike(activityIdDTO.ActivityId, this.GetUserId());
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
