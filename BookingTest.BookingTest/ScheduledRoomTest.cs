using BookingTest.BLL.Automapper;
using BookingTest.BLL.Data.Exceptions;
using BookingTest.BLL.Helper;
using BookingTest.BLL.Services;
using BookingTest.DatabaseSeed;
using BookingTest.DLL.Database;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BookingTest.BookingTest
{
    public class ScheduledRoomTest
    {
        private DbContextOptions<ApplicationDataContext> dbContextOptions = new DbContextOptionsBuilder<ApplicationDataContext>()
        .UseInMemoryDatabase(databaseName: "BookingTest")
        .Options;
        private RoomService roomService;
        private UserService userService;
        private ScheduledRoomService scheduledRoomService;

        [SetUp]
        public void Setup()
        {
            var context = new ApplicationDataContext(dbContextOptions);
            roomService = new RoomService(context);
            userService = new UserService(context, new JWTHelper(new BLL.Data.JWTAuthentication() { JwtAudience = "", JwtKey = "Booking sekckckckret key 2219-10", JwtHours = "240", JwtIssuer = "sdasd" }));
            scheduledRoomService = new ScheduledRoomService(context);
            try
            {
                ConvertersInit.Init();
            }
            catch (Exception ex)
            {

            }
            Seeder.SeedAsync(roomService, userService).GetAwaiter().GetResult();

        }
        [Test]
        public async Task Is_Room_Equal_2()
        {
            var result = roomService.GetAll();
            Assert.AreEqual(result.Count(), 2);
        }
        [Test]
        public async Task Is_Reservation_Available()
        {
            var room = roomService.GetAll().FirstOrDefault();
            var result = await scheduledRoomService.CreateAsync(new DLL.Entities.ScheduledRoom()
            {
                DateBeginScheduled = DateTime.UtcNow,
                DateEndScheduled = DateTime.UtcNow.AddDays(1),
                UserId = 1,
                RoomId = room.Id,
            });
            Assert.Greater(result.Id, 0);
        }
        [Test]
        public async Task Is_Reservation_Not_Available()
        {
            var room = roomService.GetAll().FirstOrDefault();
            var DateBeginScheduled = DateTime.UtcNow.Date;
            var DateEndScheduled = DateTime.UtcNow.AddDays(1).Date; 
            var ex = Assert.Throws<AppException>(() => scheduledRoomService.CreateAsync(new DLL.Entities.ScheduledRoom()
            {
                DateBeginScheduled = DateBeginScheduled,
                DateEndScheduled = DateEndScheduled,
                UserId = 1,
                RoomId = room.Id,
            }).GetAwaiter().GetResult());
            Assert.That(ex.Message, Is.EqualTo("The room is reserved on this date"));
        }
    }
}
