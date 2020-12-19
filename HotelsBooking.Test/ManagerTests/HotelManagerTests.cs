using System.Collections.Generic;
using System.Linq;
using Infrastructure;
using Infrastructure.EF;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Assert = Xunit.Assert;

namespace HotelsBooking.Test
{
    public class HotelManagerTests
    {
        private readonly Hotel _hotel;
        private readonly ApplicationDbContext _dbContext;
        
        public HotelManagerTests()
        {
            _hotel = new Hotel()
            {
                Id = 1,
                Description = "Lorem Ipsum Description",
                Location = "New-York",
                Name = "Trump Hotel",
                Season = Enums.Season.Demiseason,
                HotelConvs = new List<HotelConv>() { new HotelConv() { Id = 1, Price = 3400, HotelId = 1 } }
            
            };
            
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "AranozDB")
                .Options;
            
            _dbContext = new ApplicationDbContext(options);
        }
        
        [Fact]
        public void GetHotelById_ReturnsHotel()
        {
            // Seed data
            _dbContext.Hotels.Add(_hotel);
            _dbContext.SaveChanges();

            //assert
            Assert.Equal(1, _dbContext.Hotels.FirstOrDefault(x => x.Id == 1).Id);

            ClearDbContext();
        }
        
        [Fact]
        public void CreateHotel_ReturnNotNull()
        {
            // Seed data
            _dbContext.Hotels.Add(_hotel);
            _dbContext.SaveChanges();
            var existingHotel = _dbContext.Hotels.FirstOrDefault(x => x.Id == 1);
            
            //assert
            Assert.NotNull(existingHotel);

            ClearDbContext();
        }
        
        [Fact]
        public void DeleteByIdAsync_ReturnTrue()
        {
            // Seed data
            _dbContext.Hotels.Add(_hotel);
            _dbContext.SaveChanges();

            var hotelToRemove = _dbContext.Hotels.FirstOrDefault(c => c.Id == 1);
            _dbContext.Remove(hotelToRemove);
            _dbContext.SaveChanges();

            //assert
            Assert.Null(_dbContext.Hotels.FirstOrDefault(x => x.Id == 1));
        }
        
        [Fact]
        public void Update_nullHotelDto_ReturnNull()
        {
            // Seed data
            _dbContext.Hotels.Add(_hotel);
            _dbContext.SaveChanges();
            _hotel.Name = "Updated Trump Hotel";
            _dbContext.Hotels.Update(_hotel);

            //assert
            Assert.NotEqual("Trump Hotel", _dbContext.Hotels.FirstOrDefault(x => x.Id == 1).Name);
            
            ClearDbContext();
        }
        
        [Fact]
        public void UpdateHotel_NameNotTheSame_ReturnTrue()
        {
            // Seed data
            _dbContext.Hotels.Add(_hotel);
            _dbContext.SaveChanges();
            _hotel.Name = "Updated Trump Hotel";
            _dbContext.Hotels.Update(_hotel);
            
            //assert
            Assert.NotNull(_dbContext.Hotels.FirstOrDefault(x => x.Id == _hotel.Id));
            Assert.NotEqual("Trump Hotel", _dbContext.Hotels.FirstOrDefault(x => x.Name == _hotel.Name).Name);
            
            ClearDbContext();
        }

        private void ClearDbContext()
        {
            _dbContext.Hotels.Remove(_hotel);
            _dbContext.SaveChanges();
        }
    }
}