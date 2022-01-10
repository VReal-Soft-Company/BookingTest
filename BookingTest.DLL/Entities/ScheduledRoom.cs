using BookingTest.DLL.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookingTest.DLL.Entities
{
    public class ScheduledRoom:BaseEntity
    {
        public DateTime DateBeginScheduled { get; set; }
        public DateTime DateEndScheduled { get; set; }

        public long UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public long RoomId { get; set; }
        [ForeignKey(nameof(RoomId))]
        public Room Room { get; set; }

        public string AdditionalInformation { get; set; }
    }
}
