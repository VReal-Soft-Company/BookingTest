using System;
using System.Collections.Generic;
using System.Text;

namespace BookingTest.DTO.Room.Input
{
    public class FilterScheduleRoomDTO
    {
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long? RoomId { get; set; }
    }
}
