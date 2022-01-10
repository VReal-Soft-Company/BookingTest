using BookingTest.BLL.Automapper;
using BookingTest.BLL.Data.Exceptions;
using BookingTest.BLL.Services.Base;
using BookingTest.DLL.Database;
using BookingTest.DLL.Entities;
using BookingTest.DTO.Room;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingTest.BLL.Services
{
    public interface IScheduledRoomService : IBaseService<ScheduledRoom>
    {
        List<RoomDTO> GetRoomAvailableRooms(DateTime? beginDate, DateTime? endDate, long? IdRoom);
        bool IsItAvailableToSchedule(DateTime? beginDate, DateTime? endDate, long? IdRoom);
        IQueryable<ScheduledRoom> GetRoomsReservation(DateTime? beginDate, DateTime? endDate, long? IdRoom);
    }
    public class ScheduledRoomService : BaseService<ScheduledRoom>, IScheduledRoomService
    {
        public ScheduledRoomService(ApplicationDataContext contex) : base(contex)
        {
        }
        public override Task<ScheduledRoom> CreateAsync(ScheduledRoom model)
        {
            if ( IsItAvailableToSchedule(model.DateBeginScheduled, model.DateEndScheduled, model.RoomId))
            { 

                return base.CreateAsync(model);
            }
            else
            {
                throw new AppException("The room is reserved on this date");
            }
        }
        public List<RoomDTO> GetRoomAvailableRooms(DateTime? beginDate, DateTime? endDate, long? IdRoom)
        {
            var query = GetRoomsReservation(beginDate, endDate, IdRoom);
            return query.Select(f => f.Room).Distinct().ToList().ToDTO();
        }
        public IQueryable<ScheduledRoom> GetRoomsReservation(DateTime? beginDate, DateTime? endDate, long? IdRoom)
        {
            var query = GetAll();
            if (beginDate.HasValue)
            {
                query = query.Where(f => f.DateBeginScheduled.Date >= beginDate.Value.Date);
            }
            if (endDate.HasValue)
            {
                query = query.Where(f => f.DateEndScheduled.Date <= endDate.Value.Date);
            }
            if (IdRoom.HasValue)
            {
                query.Where(f => f.RoomId == IdRoom);
            }
            return query;
        }
        public bool IsItAvailableToSchedule(DateTime? beginDate, DateTime? endDate, long? IdRoom)
        {
          return GetRoomAvailableRooms(beginDate, endDate, IdRoom).Count <=0;
        }
        public override IQueryable<ScheduledRoom> GetAll()
        {
            return base.GetAll().Include(f=>f.Room);
        }
    } 
}
