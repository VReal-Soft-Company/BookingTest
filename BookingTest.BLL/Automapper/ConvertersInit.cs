
using AutoMapper;
using BookingTest.DLL.Entities;
using BookingTest.DTO.Room;
using BookingTest.DTO.RoomSchedule;
using BookingTest.DTO.User;
using System;
using System.Linq;

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
                config.CreateMap<RoomDTO, Room>().ReverseMap()
                .ForMember(f => f.ImagesIds, opt => opt.MapFrom(f => f.Images.Select(image => image.Id)));
                config.CreateMap<ScheduledRoom, ReservationsOfRoomDTO>();
            });
        }
    }
}
