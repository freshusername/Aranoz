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
       
        public HotelManager(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<HotelDTO> GetHotelById(int Id)
        {
            Hotel hotel = _context.Hotels.Include(h => h.HotelRooms)
                                            .ThenInclude(hr => hr.Room)
                                        .Include(h => h.HotelRooms)
                                                .ThenInclude(hr => hr.RoomConvs)
                                        .Include(h => h.HotelPhotos)
                                        .FirstOrDefault(h => h.Id == Id);
            return _mapper.Map<Hotel, HotelDTO>(hotel);
        }
        public IEnumerable<HotelDTO> GetHotels(FilterHotelDto filterHotelDto = null)
        {
            var hotels = _context.Hotels.Include(h => h.HotelRooms)
                                            .ThenInclude(hr => hr.Room)
                                        .Include(h => h.HotelRooms)
                                                .ThenInclude(hr => hr.RoomConvs)
                                        .Include(h => h.HotelPhotos)
                                    .Select(h => h);
            if (!String.IsNullOrEmpty(filterHotelDto?.KeyWord))
            {
                hotels = hotels.Where(h => h.Name.Contains(filterHotelDto.KeyWord)
                                    || h.Description.Contains(filterHotelDto.KeyWord)
                                    || h.Location.Contains(filterHotelDto.KeyWord));
            }
            
            if (filterHotelDto?.MinPrice >= 0 && filterHotelDto?.MaxPrice > 0)
            {
                hotels = hotels.Where(h => h.HotelRooms.Where(p => p.Price >= filterHotelDto.MinPrice && p.Price <= filterHotelDto.MaxPrice).Any());
            }

            return _mapper.Map<IEnumerable<Hotel>, IEnumerable<HotelDTO>>(hotels.ToList());
        }

        public async Task<OperationDetails> Create(HotelDTO hotelDTO)
        {
            Hotel hotelCheck = _context.Hotels.FirstOrDefault(x => x.Name == hotelDTO.Name);
            if (hotelCheck == null)
            {
                Hotel hotel = _mapper.Map<HotelDTO, Hotel>(hotelDTO);
                await _context.Hotels.AddAsync(hotel);
                await _context.SaveChangesAsync();
                return new OperationDetails(true, "Hotel added", "Name");
            }
            return new OperationDetails(false, "Hotel with the same name already exists", "Name");
        }
        public async Task<OperationDetails> Update(HotelDTO hotelDTO)
        {
            Hotel hotelCheck = _context.Hotels.FirstOrDefault(x => x.Name == hotelDTO.Name && x.Id != hotelDTO.Id);
            if (hotelCheck == null)
            {
                Hotel hotel = await _context.Hotels.FindAsync(hotelDTO.Id);
                hotel.Name = hotelDTO.Name;
                hotel.Location = hotelDTO.Location;
                hotel.Season = hotelDTO.Season;
                _context.Hotels.Update(hotel);
                await _context.SaveChangesAsync();
                return new OperationDetails(true, "Hotel update", "Name");
            }
            return new OperationDetails(false, "Hotel with the same name already exists", "Name");
        }

        public async Task Delete(int Id)
        {
            Hotel hotel = _context.Hotels.Find(Id);
            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();
        }

        #region HotelConvs

        public IEnumerable<HotelConvDTO> GetHotelConvs()
        {
            List<HotelConv> hotelConvs = _context.HotelConvs.ToList();
            List<AdditionalConv> addConvs = _context.AdditionalConvs.ToList();
            var query = hotelConvs.Join(addConvs,
                hc => hc.AdditionalConvId,
                ac => ac.Id,
                (hc, ac) => new HotelConvDTO { Id = hc.Id, Name = ac.Name, HotelId = hc.HotelId, Price = hc.Price }
                );
            return query;
        }

        public async Task<OperationDetails> CreateHotelConv(HotelConvDTO hotelConvDTO)
        {
            
            HotelConv check = _context.HotelConvs.FirstOrDefault(x => x.AdditionalConv.Name == hotelConvDTO.Name && x.HotelId==hotelConvDTO.HotelId);
            if (check == null)
            {
                HotelConv hotelConv = new HotelConv 
                {
                    Price = hotelConvDTO.Price,
                    HotelId = hotelConvDTO.HotelId,
                    Hotel = await _context.Hotels.FirstAsync(x=>x.Id==hotelConvDTO.HotelId),
                    AdditionalConv = await _context.AdditionalConvs.FirstAsync(x=>x.Name==hotelConvDTO.Name),
                    AdditionalConvId =  _context.AdditionalConvs.First(x=>x.Name==hotelConvDTO.Name).Id
                };
                await _context.HotelConvs.AddAsync(hotelConv);
                await _context.SaveChangesAsync();
                return new OperationDetails(true, "Hotel convenience added", "Name");
            }
            return new OperationDetails(false, "Hotel convenience with the same name already exists", "Name");
        }

        public async Task DeleteHotelConv(int Id)
        {
            HotelConv hotelConv = _context.HotelConvs.Find(Id);
            _context.HotelConvs.Remove(hotelConv);
            await _context.SaveChangesAsync();
        }
        #endregion

        public void Dispose()
        {
            
        }
    }
}
