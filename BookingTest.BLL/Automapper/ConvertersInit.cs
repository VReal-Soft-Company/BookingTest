
using AutoMapper;
using BookingTest.DLL.Entities;
using BookingTest.DTO.Room;
using BookingTest.DTO.User;
using System;

namespace BookingTest.BLL.Automapper
{
    public class ConvertersInit
    { 
        public static void Init()
        {
            Mapper.Initialize((config) =>
            {
                config.CreateMap<RegisterDTO, User>();
                config.CreateMap<LoginDTO, User>();
                config.CreateMap<RoomDTO, Room>().ReverseMap();
            });
        }
    }
}
