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
        private readonly DbSet<HotelConv> _hotelConvs;
        private readonly DbSet<AdditionalConv> _additionalConvs;
        public HotelManager(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _hotels = _context.Hotels;
            _hotelConvs = _context.HotelConvs;
            _additionalConvs = _context.AdditionalConvs;
            _mapper = mapper;
        }
        public async Task<Hotel> GetHotelById(int Id) => await _hotels.FindAsync(Id);
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
        public async Task<OperationDetails> Update(HotelDTO hotelDTO)
        {
            Hotel hotelCheck = _hotels.FirstOrDefault(x => x.Name == hotelDTO.Name && x.Id != hotelDTO.Id);
            if (hotelCheck == null)
            {
                Hotel hotel = await _hotels.FindAsync(hotelDTO.Id);
                hotel.Name = hotelDTO.Name;
                hotel.Location = hotelDTO.Location;
                hotel.Season = hotelDTO.Season;
                _hotels.Update(hotel);
                await _context.SaveChangesAsync();
                return new OperationDetails(true, "Hotel update", "Name");
            }
            return new OperationDetails(false, "Hotel with the same name already exists", "Name");
        }
        public async Task Delete(int Id)
        {
            Hotel hotel = _hotels.Find(Id);
            _hotels.Remove(hotel);
            await _context.SaveChangesAsync();
        }
        #region HotelConvs
        public IEnumerable<HotelConvDTO> GetHotelConvs()
        {
            List<HotelConv> hotelConvs = _hotelConvs.ToList();
            List<AdditionalConv> addConvs = _additionalConvs.ToList();
            var query = hotelConvs.Join(addConvs,
                hc => hc.AdditionalConvId,
                ac => ac.Id,
                (hc, ac) => new HotelConvDTO { Id = hc.Id, Name = ac.Name, HotelId = hc.HotelId, Price = hc.Price }
                );
            return query;
        }
        public async Task<OperationDetails> CreateHotelConv(HotelConvDTO hotelConvDTO)
        {
            
            HotelConv check = _hotelConvs.FirstOrDefault(x => x.AdditionalConv.Name == hotelConvDTO.Name && x.HotelId==hotelConvDTO.HotelId);
            if (check == null)
            {
                HotelConv hotelConv = new HotelConv 
                {
                    Price = hotelConvDTO.Price,
                    HotelId = hotelConvDTO.HotelId,
                    Hotel = await _hotels.FirstAsync(x=>x.Id==hotelConvDTO.HotelId),
                    AdditionalConv = await _additionalConvs.FirstAsync(x=>x.Name==hotelConvDTO.Name),
                    AdditionalConvId =  _additionalConvs.First(x=>x.Name==hotelConvDTO.Name).Id
                };
                await _hotelConvs.AddAsync(hotelConv);
                await _context.SaveChangesAsync();
                return new OperationDetails(true, "Hotel convenience added", "Name");
            }
            return new OperationDetails(false, "Hotel convenience with the same name already exists", "Name");
        }
        #endregion
        public void Dispose()
        {
            
        }
    }
}
