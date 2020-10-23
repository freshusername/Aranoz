using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.EF
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseMySql(@"server=localhost;database=hotelsdb;uid=root;password=admin;");
        //}

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<HotelRoom> HotelRooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<AdditionalConv> AdditionalConvs { get; set; }
        public DbSet<RoomConv> RoomConvs { get; set; }
        public DbSet<HotelConv> HotelConvs { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderConv> OrderConvs { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<HotelRoom>()
                .HasOne(pt => pt.Hotel)
                .WithMany(p => p.HotelRooms)
                .HasForeignKey(pt => pt.HotelId);

            modelBuilder.Entity<HotelRoom>()
                .HasOne(pt => pt.Room)
                .WithMany(t => t.HotelRooms)
                .HasForeignKey(pt => pt.RoomId);
        }
    }
}
