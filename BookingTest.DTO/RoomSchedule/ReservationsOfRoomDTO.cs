using System;
using System.Collections.Generic;
using System.Text;

namespace BookingTest.DTO.RoomSchedule
{
    public class ReservationsOfRoomDTO
    {
        public long RoomId { get; set; }
        public DateTime DateBeginScheduled { get; set; }
        public DateTime DateEndScheduled { get; set; }

    }
}
