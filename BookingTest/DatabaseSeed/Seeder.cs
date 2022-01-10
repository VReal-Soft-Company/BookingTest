using BookingTest.BLL.Services;
using BookingTest.DLL.Database;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;

namespace BookingTest.DatabaseSeed
{
    public static class Seeder
    {
        public static async Task SeedAsync(IRoomService roomService, IUserService userService)
        { 
            if (!await userService.AnyAsync())
            {
                await userService.RegisterAsync(new DTO.User.RegisterDTO() { Email = "userTest1@gmail.com", Password = "qwerty1234" });
                await userService.RegisterAsync(new DTO.User.RegisterDTO() { Email = "userTest2@gmail.com", Password = "qwerty1234" });
            } 
            if(!await roomService.AnyAsync())
            {
                await roomService.CreateAsync(new DLL.Entities.Room()
                {
                    Title = "Test room",
                    Price = 100,
                    Description = "Test descriptiion",
                    CapacityOfRoom = 2,
                    Address = "City Cherkassy"
                });
                await roomService.CreateAsync(new DLL.Entities.Room()
                {
                    Title = "Test room2",
                    Price = 100,
                    Description = "Test descriptiion",
                    CapacityOfRoom = 2,
                    Address = "City Cherkassy"
                });
            }
        }

    }
}
