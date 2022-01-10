using BookingTest.BLL.Automapper;
using BookingTest.BLL.Services;
using BookingTest.Data.Extensions;
using BookingTest.DTO.Room.Input;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BookingTest.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly IImageService _imageService;
        private readonly IScheduledRoomService _scheduledRoomService;

        public RoomController(IRoomService roomService, IImageService imageService, IScheduledRoomService scheduledRoomService)
        {
            _roomService = roomService;
            _scheduledRoomService = scheduledRoomService;
            _imageService = imageService;
        }

        [HttpPost("Schedule")]
        public async Task<IActionResult> ScheduleRoom([FromBody] ScheduleRoomDTO model)
        {
            if (ModelState.IsValid)
            {
                if (!model.EndDate.HasValue)
                {
                    model.EndDate = model.BeginDate;
                }
                await _scheduledRoomService.CreateAsync(new DLL.Entities.ScheduledRoom()
                {
                    DateBeginScheduled = model.BeginDate.Date,
                    DateEndScheduled = model.EndDate.Value.Date,
                    RoomId = model.RoomId,
                    UserId = HttpContext.GetCurrentUserID(),
                    AdditionalInformation = model.AdditionalInformation
                });
            }
            return BadRequest(ModelState);
        }
        [HttpDelete("Schedule")]
        public async Task<IActionResult> RemoveSchedule(long Id)
        {
            if (ModelState.IsValid)
            {
                var schedule = await _scheduledRoomService.FirstOrDefaultAsync(f => f.Id == Id);
                if (schedule != null || schedule.UserId != HttpContext.GetCurrentUserID())
                {
                    return Forbid();
                }
                await _scheduledRoomService.DeleteAsync(Id);
            }
            return BadRequest(ModelState);
        }
        [AllowAnonymous]
        [HttpGet("{roomId}/{imageId}/picture.jpg")]
        [ProducesResponseType(typeof(void), 200)]
        public async Task<IActionResult> GetPicture(long roomId, long imageId)
        { 
            return File(await _imageService.GetImageStreamAsync(roomId, imageId), "image/jpeg", true);
        }
         [AllowAnonymous]
        [HttpPost("{idOfRoom}/picture")]
        public async Task<IActionResult> UploadPicture(long idOfRoom, [FromForm(Name = "file")]IFormFile file)
        {

            if (file == null)
            {
                throw new Exception("You don't specified article picture!");
            }

            await _imageService.SaveImageAsync(new DLL.Entities.Images()
            {
                DateTime = DateTime.UtcNow,
                RoomId = idOfRoom,
                Name = file.FileName,
                Size = file.Length,
            }, file.OpenReadStream());


            return Ok();
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetRooms()
        {
            return Ok(_roomService.GetAll().ToList().ToDTO());
        }
    }
}
