using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure;
using Infrastructure.EF;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Assert = Xunit.Assert;

namespace HotelsBooking.Test.ManagerTests
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
                Season = Enums.Season.Demiseason
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

        [Fact]
        public void GetHotelConvs_ReturnTrue()
        {
            // Seed data
            _dbContext.Hotels.Add(_hotel);
            _dbContext.SaveChanges();
            
            _dbContext.HotelConvs.AddRange(GenerateHotelConvs(3, _hotel.Id));
            _dbContext.SaveChanges();
            
            //assert
            Assert.NotNull(_dbContext.HotelConvs.FirstOrDefault(x => x.HotelId == _hotel.Id));
            Assert.Equal(3, _dbContext.HotelConvs.Count());
            
            ClearDbContext();
        }
        
        [Fact]
        public void GetAdditionalConvs_ReturnTrue()
        {
            // Seed data
            _dbContext.Hotels.Add(_hotel);
            _dbContext.SaveChanges();
            
            _dbContext.AdditionalConvs.AddRange(GenerateAdditionalConvs(3));
            _dbContext.SaveChanges();
            
            //assert
            Assert.NotNull(_dbContext.AdditionalConvs.FirstOrDefault(x => x.Name.Contains("Additional Conv")));
            Assert.Equal(3, _dbContext.AdditionalConvs.Count());
            
            ClearDbContext();
        }

        private void ClearDbContext()
        {
            if(_dbContext.Hotels.FirstOrDefault().Id == _hotel.Id)
                _dbContext.Hotels.Remove(_hotel);
            
            if(3 == _dbContext.HotelConvs.Count(x => x.HotelId == _hotel.Id))
                foreach (var c in _dbContext.HotelConvs)
                    _dbContext.HotelConvs.Remove(c);
            
            if(3 == _dbContext.AdditionalConvs.Count(x => x.Name.Contains("Additional Conv")))
                foreach (var c in _dbContext.HotelConvs)
                    _dbContext.HotelConvs.Remove(c);
            
            _dbContext.SaveChanges();
        }
        
        private List<HotelConv> GenerateHotelConvs(int count, int hotelId)
        {
            var convs = new List<HotelConv>();
            Random rand = new Random();
            
            for (int i = 0; i < count; i++)
                convs.Add(new HotelConv() { Price = rand.Next(250, 1500), HotelId = _hotel.Id });

            return convs;
        }
        
        private List<AdditionalConv> GenerateAdditionalConvs(int count)
        {
            var convs = new List<AdditionalConv>();
            Random rand = new Random();
            
            for (int i = 0; i < count; i++)
                convs.Add(new AdditionalConv() { Name = $"Additional Conv {i}"});

            return convs;
        }
    }
}