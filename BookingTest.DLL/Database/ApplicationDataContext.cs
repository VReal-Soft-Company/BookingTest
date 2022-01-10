using BookingTest.DLL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingTest.DLL.Database
{
    public class ApplicationDataContext : DbContext
    {


        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<ScheduledRoom> ScheduledRooms { get; set; }
        public DbSet<Images> Images { get; set; }

        public ApplicationDataContext()
        {

        }
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
          .HasMany(b => b.ScheduledRooms)
          .WithOne(f => f.User)
          .HasForeignKey(b => b.UserId)
          .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Room>()
        .HasMany(b => b.ScheduledRooms)
        .WithOne(f => f.Room)
        .HasForeignKey(b => b.RoomId)
        .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
