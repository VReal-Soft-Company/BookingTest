﻿// <auto-generated />
using System;
using BookingTest.DLL.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BookingTest.DLL.Migrations
{
    [DbContext(typeof(ApplicationDataContext))]
    [Migration("20220106150902_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("BookingTest.DLL.Entities.Room", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<ushort>("CapacityOfRoom");

                    b.Property<string>("Description");

                    b.Property<bool>("IsDeleted");

                    b.Property<double>("Price");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("BookingTest.DLL.Entities.ScheduledRoom", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AdditionalInformation");

                    b.Property<DateTime>("DateBeginScheduled");

                    b.Property<DateTime>("DateEndScheduled");

                    b.Property<bool>("IsDeleted");

                    b.Property<long>("RoomId");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.HasIndex("UserId");

                    b.ToTable("ScheduledRooms");
                });

            modelBuilder.Entity("BookingTest.DLL.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastName");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("Role");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BookingTest.DLL.Entities.ScheduledRoom", b =>
                {
                    b.HasOne("BookingTest.DLL.Entities.Room", "Room")
                        .WithMany("ScheduledRooms")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BookingTest.DLL.Entities.User", "User")
                        .WithMany("ScheduledRooms")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
