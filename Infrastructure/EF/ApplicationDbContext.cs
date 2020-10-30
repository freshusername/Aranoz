using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Infrastructure.Enums;

namespace Infrastructure.EF
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<HotelRoom> HotelRooms { get; set; }
        public DbSet<HotelPhoto> HotelPhotos { get; set; }
        public DbSet<AdditionalConv> AdditionalConvs { get; set; }
        public DbSet<RoomConv> RoomConvs { get; set; }
        public DbSet<HotelConv> HotelConvs { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
                .Entity<Hotel>()
                .Property(p => p.Season)
                .HasConversion(
                v => v.ToString(),
                v => (Season)Enum.Parse(typeof(Season), v));
            //modelBuilder.Entity<HotelRoom>()
            //    .HasOne(pt => pt.Hotel)
            //    .WithMany(p => p.HotelRooms)
            //    .HasForeignKey(pt => pt.HotelId);

            //modelBuilder.Entity<HotelRoom>()
            //    .HasOne(pt => pt.Room)
            //    .WithMany(t => t.HotelRooms)
            //    .HasForeignKey(pt => pt.RoomId);
        }
    }
}
