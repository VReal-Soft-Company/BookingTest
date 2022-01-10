using BookingTest.DLL.Entities.Base;
using System.Collections.Generic;

namespace BookingTest.DLL.Entities
{
    public class Room : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public ushort CapacityOfRoom { get; set; }
        public double Price { get; set; }
        public ICollection<ScheduledRoom> ScheduledRooms { get; set; }
    }
}
