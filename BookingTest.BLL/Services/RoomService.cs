using BookingTest.BLL.Services.Base;
using BookingTest.DLL.Database;
using BookingTest.DLL.Entities;
using BookingTest.DTO.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookingTest.BLL.Services
{
    public interface IRoomService:IBaseService<Room>
    { 
    }
    public class RoomService : BaseService<Room>, IRoomService
    {
        public RoomService(ApplicationDataContext contex) : base(contex)
        {
        }
    }
}
