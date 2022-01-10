using BookingTest.BLL.Automapper;
using BookingTest.BLL.Services;
using BookingTest.Data.Extensions;
using BookingTest.DTO.Room.Input;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IScheduledRoomService _scheduledRoomService;

        public RoomController(IRoomService roomService, IScheduledRoomService scheduledRoomService)
        {
            _roomService = roomService;
            _scheduledRoomService = scheduledRoomService;
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
        public async Task<IActionResult> RemoveSchedule( long Id)
        {
            if (ModelState.IsValid)
            {
                var schedule = await _scheduledRoomService.FirstOrDefaultAsync(f => f.Id == Id);
                if (schedule != null ||  schedule.UserId!= HttpContext.GetCurrentUserID()) {
                    return Forbid();
                }
                await _scheduledRoomService.DeleteAsync(Id);
            }
            return BadRequest(ModelState);
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetRooms( )
        {
            return Ok(_roomService.GetAll().ToList().ToDTO());
        }
    }
}
