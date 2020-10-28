using ApplicationCore.DTOs;
using ApplicationCore.Interfaces;
using AutoMapper;
using Infrastructure.EF;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ApplicationCore.Services
{
    public class HotelManager : IHotelManager
    {
        protected readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public HotelManager(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //load data from relative tables(available hotel convs, etc.)
        public HotelDto Get(int hotelId)
        {
            Hotel hotel = _context.Hotels.Include(h => h.HotelRooms)
                                            .ThenInclude(hr => hr.Room)
                                        .Include(h => h.HotelRooms)
                                                .ThenInclude(hr => hr.RoomConvs)
                                        .Include(h => h.HotelPhotos)
                                        .FirstOrDefault(h => h.Id == hotelId);
            return _mapper.Map<Hotel, HotelDto>(hotel);
        }

       
        public IEnumerable<HotelDto> GetHotels()
        {
            IEnumerable<Hotel> hotels = _context.Hotels.ToList();
            return _mapper.Map<IEnumerable<Hotel>, IEnumerable<HotelDto>>(hotels);
        }
        
        public void Insert(HotelDto hotel)
        {
            Hotel hotel_to_add = _mapper.Map<HotelDto, Hotel>(hotel);
            _context.Hotels.Add(hotel_to_add);
            _context.SaveChanges();
        }

        public void Update(HotelDto hotel)
        {
            Hotel hotel_to_update = _mapper.Map<HotelDto, Hotel>(hotel);
            try
            {
                _context.Hotels.Attach(hotel_to_update);
            }
            catch { }
            finally
            {
                _context.Hotels.Update(hotel_to_update);
            }
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            Hotel hotel_to_delete = _context.Hotels.Find(id);
            if (_context.Entry(hotel_to_delete).State == EntityState.Detached)
            {
                _context.Hotels.Attach(hotel_to_delete);
            }
            _context.Hotels.Remove(hotel_to_delete);
        }
    }
}
