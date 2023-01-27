using MakerHUB.API.Extensions;
using MakerHUB.BLL.DTO;
using MakerHUB.BLL.Exceptions;
using MakerHUB.BLL.Services.MeetingServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MakerHUB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingController : ControllerBase
    {
        private readonly MeetingService _meetingService;

        public MeetingController(MeetingService meetingService)
        {
            _meetingService = meetingService;
        }

        [HttpGet]
        [Authorize]
        public ActionResult GetAll()
        {
            IEnumerable<MeetingDTO> meetingDTOs = _meetingService.GetAll(this.GetUserId());
            return Ok(meetingDTOs);
        }

        [HttpGet("upcoming-meetings")]
        [Authorize]
        public ActionResult GetUpcomingMeetings()
        {
            IEnumerable<MeetingDTO> meetingDTOs = _meetingService.GetUpcomingMeetings(this.GetUserId());
            return Ok(meetingDTOs);
        }

        [HttpGet("{meetingId}")]
        [Authorize]
        public ActionResult<ActivityDTO> Get(int meetingId)
        {
            try
            {
                MeetingDTO meetingDTO = _meetingService.GetById(meetingId, this.GetUserId());
                return Ok(meetingDTO);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add([FromBody] MeetingAddDTO meetingAddDTO)
        {
            try
            {
                int meetingId = _meetingService.Create(meetingAddDTO, this.GetUserId());
                return Ok(meetingId);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Authorize]
        public IActionResult Modify(MeetingEditDTO meetingEditDTO)
        {
            try
            {
                _meetingService.Update(meetingEditDTO, this.GetUserId());
                return NoContent();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
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

        [HttpPost("meeting-subscribe")]
        [Authorize]
        public IActionResult Subscribe([FromBody] MeetingIdDTO meetingIdDTO)
        {
            try
            {
                _meetingService.Subscribe(meetingIdDTO.MeetingId, this.GetUserId());
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (AlreadyExistException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{meetingId}")]
        [Authorize]
        public IActionResult Delete(int meetingId)
        {
            try
            {
                _meetingService.Delete(meetingId, this.GetUserId());
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ForbiddenException ex)
            {
                return StatusCode(403, ex.Message);
            }

            return NoContent();
        }

        [HttpPost("meeting-unsubscribe")]
        [Authorize]
        public IActionResult Unsubscribe([FromBody] MeetingIdDTO meetingIdDTO)
        {
            try
            {
                _meetingService.Unsubscribe(meetingIdDTO.MeetingId, this.GetUserId());
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("meeting-like")]
        [Authorize]
        public IActionResult Like([FromBody] MeetingIdDTO meetingIdDTO)
        {
            try
            {
                _meetingService.Like(meetingIdDTO.MeetingId, this.GetUserId());
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

        [HttpPost("meeting-dislike")]
        [Authorize]
        public IActionResult Dislike([FromBody] MeetingIdDTO meetingIdDTO)
        {
            try
            {
                _meetingService.Dislike(meetingIdDTO.MeetingId, this.GetUserId());
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
