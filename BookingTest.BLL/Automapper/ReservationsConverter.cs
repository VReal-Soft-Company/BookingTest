using AutoMapper;
using BookingTest.DLL.Entities;
using BookingTest.DTO.Room;
using BookingTest.DTO.RoomSchedule;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingTest.BLL.Automapper
{
   public static class ReservationsConverter
    {
        public static List<ReservationsOfRoomDTO> ToDTO<T>(this List<T> item) where T : ScheduledRoom
        {
            return Mapper.Map<List<ReservationsOfRoomDTO>>(item);
        }
    }
}
