using System;
using System.Collections.Generic;
using System.Text;

namespace BookingTest.DTO.Room
{
    public class RoomDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public ushort CapacityOfRoom { get; set; }
        public double Price { get; set; }
        public ICollection<long> ImagesIds { get; set; }
    }
}
