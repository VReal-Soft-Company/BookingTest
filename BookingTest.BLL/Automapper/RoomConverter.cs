using AutoMapper;
using BookingTest.DLL.Entities;
using BookingTest.DTO.Room;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingTest.BLL.Automapper
{
    public static class RoomConverter
    {
        public static Room ToEntity<T>(this T item) where T : RoomDTO
        {
            return Mapper.Map<Room>(item);
        }
        public static RoomDTO ToDTO<T>(this T item) where T : Room
        {
            return  Mapper.Map<RoomDTO>(item);
        }
        public static List<RoomDTO> ToDTO<T>(this List<T> item) where T : Room
        {
            return Mapper.Map<List<RoomDTO>>(item);
        }
    }
}
