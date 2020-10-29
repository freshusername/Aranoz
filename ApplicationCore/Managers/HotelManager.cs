using ApplicationCore.DTOs;
using ApplicationCore.Infrastructure;
using ApplicationCore.Interfaces;
using AutoMapper;
using Infrastructure.EF;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Managers
{
    public class HotelManager : IHotelManager
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;
        private readonly DbSet<Hotel> _hotels;
        public HotelManager(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _hotels = _context.Hotels;
            _mapper = mapper;
        }
        public List<Hotel> GetHotels() => _hotels.ToList();
        public async Task<OperationDetails> Create(HotelDTO hotelDTO)
        {
            Hotel hotelCheck = _hotels.FirstOrDefault(x => x.Name == hotelDTO.Name);
            if (hotelCheck == null)
            {
                Hotel hotel = _mapper.Map<HotelDTO, Hotel>(hotelDTO);
                await _hotels.AddAsync(hotel);
                await _context.SaveChangesAsync();
                return new OperationDetails(true, "Hotel added", "Name");
            }
            return new OperationDetails(false, "Hotel with the same name already exists", "Name");
        }
        public async Task Delete(int Id)
        {
            Hotel hotel = _hotels.Find(Id);
            _hotels.Remove(hotel);
            await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            
        }
    }
}
