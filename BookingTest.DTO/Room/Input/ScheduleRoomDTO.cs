using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookingTest.DTO.Room.Input
{
    public class ScheduleRoomDTO
    {
        [Required]
        public DateTime BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        [Required]
        public long RoomId { get; set; }
        public string AdditionalInformation { get; set; }
    }
}
