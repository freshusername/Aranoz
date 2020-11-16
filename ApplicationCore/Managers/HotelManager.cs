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
using static Infrastructure.Enums;

namespace ApplicationCore.Managers
{
    public class HotelManager : IHotelManager
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;

        public HotelManager(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #region Hotels

        public async Task<HotelDTO> GetHotelById(int Id)
        {
            Hotel hotel = _context.Hotels.Include(h => h.HotelRooms)
                                            .ThenInclude(hr => hr.Room)
                                        .Include(h => h.HotelRooms)
                                                .ThenInclude(hr => hr.RoomConvs)
                                                .ThenInclude(rc => rc.AdditionalConv)
                                        .Include(h => h.HotelPhotos)
                                        .FirstOrDefault(h => h.Id == Id);
            return _mapper.Map<Hotel, HotelDTO>(hotel);
        }
        public IEnumerable<HotelDTO> GetHotels(FilterHotelDto filterHotelDto = null, string sortOrder=null)
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

            switch (sortOrder)
            {
                case "name_desc":
                    hotels = hotels.OrderByDescending(u => u.Name);
                    break;
                case "location":
                    hotels = hotels.OrderBy(u => u.Location)
                        .ThenBy(u=>u.Name);
                    break;
                case "location_desc":
                    hotels = hotels.OrderByDescending(u => u.Location)
                        .ThenByDescending(u => u.Name);
                    break;
                case "season":
                    hotels = hotels.OrderBy(u => u.Season)
                        .ThenBy(u => u.Name);
                    break;
                case "season_desc":
                    hotels = hotels.OrderByDescending(u => u.Season)
                        .ThenByDescending(u => u.Name);
                    break;
                default:
                    hotels = hotels.OrderBy(u => u.Name);
                    break;
            }

            return _mapper.Map<IEnumerable<Hotel>, IEnumerable<HotelDTO>>(hotels.ToList());
        }

        public async Task<OperationDetails> Create(HotelDTO hotelDTO)
        {
            Hotel hotelCheck = _context.Hotels.FirstOrDefault(x => x.Name == hotelDTO.Name && x.Location == hotelDTO.Location);
            if (hotelCheck == null)
            {
                Hotel hotel = _mapper.Map<HotelDTO, Hotel>(hotelDTO);
                await _context.Hotels.AddAsync(hotel);
                await _context.SaveChangesAsync();
                return new OperationDetails(true, "Hotel added", "Name");
            }
            return new OperationDetails(false, "Hotel with the same name and location already exists", "Name");
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
        #endregion
        #region HotelConvs

        public HotelConvDTO GetHotelConvById(int Id)
        {
            HotelConv hotelConv = _context.HotelConvs.Include(hc => hc.AdditionalConv)
                                               .FirstOrDefault(hc => hc.Id == Id);

            return _mapper.Map<HotelConv, HotelConvDTO>(hotelConv);
        }

        public IEnumerable<HotelConvDTO> GetHotelConvs(string sortOrder=null)
        {
            List<HotelConv> hotelConvs = _context.HotelConvs.ToList();
            List<AdditionalConv> addConvs = _context.AdditionalConvs.ToList();

            var query = hotelConvs.Join(addConvs,
                hc => hc.AdditionalConvId,
                ac => ac.Id,
                (hc, ac) => new HotelConvDTO { Id = hc.Id, Name = ac.Name, HotelId = hc.HotelId, Price = hc.Price }
                );

            switch (sortOrder)
            {
                case "name_desc":
                    query = query.OrderByDescending(u => u.Name);
                    break;
                case "price":
                    query = query.OrderBy(u => u.Price)
                        .ThenBy(u => u.Name);
                    break;
                case "price_desc":
                    query = query.OrderByDescending(u => u.Price)
                        .ThenByDescending(u => u.Name);
                    break;
                default:
                    query = query.OrderBy(u => u.Name);
                    break;
            }

            return query;
        }

        public async Task<OperationDetails> CreateHotelConv(HotelConvDTO hotelConvDTO)
        {

            HotelConv check = _context.HotelConvs.FirstOrDefault(x => x.AdditionalConv.Name == hotelConvDTO.Name && x.HotelId == hotelConvDTO.HotelId);
            if (check == null)
            {
                HotelConv hotelConv = new HotelConv
                {
                    Price = hotelConvDTO.Price,
                    HotelId = hotelConvDTO.HotelId,
                    Hotel = _context.Hotels.First(x => x.Id == hotelConvDTO.HotelId),
                    AdditionalConv = _context.AdditionalConvs.First(x => x.Name == hotelConvDTO.Name),
                    AdditionalConvId = _context.AdditionalConvs.First(x => x.Name == hotelConvDTO.Name).Id
                };
                await _context.HotelConvs.AddAsync(hotelConv);
                await _context.SaveChangesAsync();
                return new OperationDetails(true, "Hotel convenience added", "Name");
            }
            return new OperationDetails(false, "Hotel convenience with the same name in that hotel is already exists", "Name");
        }

        public async Task DeleteHotelConv(int Id)
        {
            HotelConv hotelConv = _context.HotelConvs.Find(Id);
            _context.HotelConvs.Remove(hotelConv);
            await _context.SaveChangesAsync();
        }

        public async Task<OperationDetails> UpdateHotelConv(HotelConvDTO hotelConvDTO)
        {
            HotelConv check = _context.HotelConvs.FirstOrDefault(x => x.AdditionalConv.Name == hotelConvDTO.Name && x.HotelId == hotelConvDTO.HotelId);
            if (check == null || check.Id == hotelConvDTO.Id)
            {

                HotelConv hotelConv = _context.HotelConvs.Find(hotelConvDTO.Id);
                hotelConv.Price = hotelConvDTO.Price;
                hotelConv.AdditionalConv = _context.AdditionalConvs.First(x => x.Name == hotelConvDTO.Name);
                hotelConv.AdditionalConvId = _context.AdditionalConvs.First(x => x.Name == hotelConvDTO.Name).Id;

                _context.HotelConvs.Update(hotelConv);
                await _context.SaveChangesAsync();
                return new OperationDetails(true, "Hotel convenience updated", "Name");
            }
            return new OperationDetails(false, "Hotel convenience with the same name in that hotel is already exists", "Name");

        }
        #endregion
        #region HotelRooms

        public HotelRoomDTO GetHotelRoomById(int Id)
        {
            HotelRoom hotelRoom = _context.HotelRooms.Include(hr => hr.Room)
                                                    .Include(hr => hr.Hotel)
                                                    .FirstOrDefault(hr => hr.Id == Id);
            return _mapper.Map<HotelRoom, HotelRoomDTO>(hotelRoom);
        }
        public IEnumerable<HotelRoomDTO> GetHotelRooms(string sortOrder = null)
        {
            List<HotelRoom> hotelRooms = _context.HotelRooms.ToList();
            List<Room> rooms = _context.Rooms.ToList();
            var query = hotelRooms.Join(rooms,
                hr => hr.RoomId,
                r => r.Id,
                (hr, r) => new HotelRoomDTO { Id = hr.Id, HotelId = hr.HotelId, Price = hr.Price, RoomId = r.Id, Type = r.RoomType, Number = hr.Number }
                );

            switch (sortOrder)
            {
                case "number_desc":
                    query = query.OrderByDescending(u => u.Number);
                    break;
                case "price":
                    query = query.OrderBy(u => u.Price)
                        .ThenBy(u => u.Number);
                    break;
                case "price_desc":
                    query = query.OrderByDescending(u => u.Price)
                        .ThenByDescending(u => u.Number);
                    break;
                case "type":
                    query = query.OrderBy(u => u.Type)
                        .ThenBy(u => u.Number);
                    break;
                case "type_desc":
                    query = query.OrderByDescending(u => u.Type)
                        .ThenByDescending(u => u.Number);
                    break;
                default:
                    query = query.OrderBy(u => u.Number);
                    break;
            }

            return query;
        }

       
        public async Task<OperationDetails> CreateHotelRoom(HotelRoomDTO hotelRoomDTO)
        {
            HotelRoom check = _context.HotelRooms.FirstOrDefault(x => x.Number == hotelRoomDTO.Number);
            if (check == null)
            {
                HotelRoom hotelRoom = new HotelRoom
                {
                    Price = hotelRoomDTO.Price,
                    Number = hotelRoomDTO.Number,
                    Hotel = _context.Hotels.First(h => h.Id == hotelRoomDTO.HotelId),
                    HotelId = _context.Hotels.First(h => h.Id == hotelRoomDTO.HotelId).Id,
                    Room = _context.Rooms.FirstOrDefault(r => r.RoomType == hotelRoomDTO.Type),
                    RoomId = _context.Rooms.FirstOrDefault(r => r.RoomType == hotelRoomDTO.Type).Id
                };
                await _context.HotelRooms.AddAsync(hotelRoom);
                await _context.SaveChangesAsync();
                return new OperationDetails(true, "Hotel room added", "Number");
            }
            return new OperationDetails(false, "Hotel room with the same number in that hotel is already exists", "Number");
        }

        public async Task<OperationDetails> UpdateHotelRoom (HotelRoomDTO hotelRoomDTO)
        {
            HotelRoom check = _context.HotelRooms.FirstOrDefault(x => x.Number == hotelRoomDTO.Number);
            if (check == null || check.Id == hotelRoomDTO.Id)
            {

                HotelRoom hotelRoom = _context.HotelRooms.Find(hotelRoomDTO.Id);
                hotelRoom.Number = hotelRoomDTO.Number;
                hotelRoom.Price = hotelRoomDTO.Price;
                hotelRoom.Room = _context.Rooms.FirstOrDefault(r => r.RoomType == hotelRoomDTO.Type);
                hotelRoom.RoomId = _context.Rooms.FirstOrDefault(r => r.RoomType == hotelRoomDTO.Type).Id;

                _context.HotelRooms.Update(hotelRoom);
                await _context.SaveChangesAsync();
                return new OperationDetails(true, "Hotel room updated", "Number");
            }
            return new OperationDetails(false, "Hotel room with the same number in that hotel is already exists", "Number");
        }

        public async Task DeleteHotelRoom(int Id)
        {
            HotelRoom hotelRoom = _context.HotelRooms.Find(Id);
            _context.HotelRooms.Remove(hotelRoom);
            await _context.SaveChangesAsync();
        }
        #endregion
        #region HotelRoomConvs
        public IEnumerable<HotelRoomConvDTO> GetHotelRoomConvs(int Id, string sortOrder = null)
        {
            IEnumerable<RoomConv> roomConvs = _context.RoomConvs.ToList().Where(rc => rc.HotelRoomId == Id);
            List<AdditionalConv> convs = _context.AdditionalConvs.ToList();

            var query = roomConvs.Join(convs,
                rc => rc.AdditionalConvId,
                c => c.Id,
                (rc, c) => new HotelRoomConvDTO { Id = rc.Id, Price = rc.Price, HotelRoomId = rc.HotelRoomId, ConvName = c.Name }
                );

            switch (sortOrder)
            {
                case "name_desc":
                    query = query.OrderByDescending(u => u.ConvName);
                    break;
                case "price":
                    query = query.OrderBy(u => u.Price)
                        .ThenBy(u => u.ConvName);
                    break;
                case "price_desc":
                    query = query.OrderByDescending(u => u.Price)
                        .ThenByDescending(u => u.ConvName);
                    break;
                default:
                    query = query.OrderBy(u => u.ConvName);
                    break;
            }
            return query;
        }

        public async Task<OperationDetails> CreateHotelRoomConv(HotelRoomConvDTO conv)
        {
            RoomConv check = _context.RoomConvs.FirstOrDefault(rc => rc.Id == conv.Id && rc.AdditionalConv.Name == conv.ConvName);
            if(check == null)
            {
                RoomConv roomConv = new RoomConv
                {
                    Price = _context.HotelConvs.First(x => x.AdditionalConv.Name == conv.ConvName).Price,
                    HotelRoom = _context.HotelRooms.First(x => x.Id == conv.HotelRoomId),
                    HotelRoomId = conv.HotelRoomId,
                    AdditionalConv = _context.AdditionalConvs.First(x => x.Name == conv.ConvName),
                    AdditionalConvId = _context.AdditionalConvs.First(x => x.Name == conv.ConvName).Id
                };

                _context.Add(roomConv);
                await _context.SaveChangesAsync();
                return new OperationDetails(true, "Room conv added", "Name");
            }
            return new OperationDetails(false, "Convenience with the same name already exists", "Name");
        }

        public async Task DeleteHotelRoomConv(int Id)
        {
            RoomConv roomConv = _context.RoomConvs.Find(Id);
            _context.RoomConvs.Remove(roomConv);
            await _context.SaveChangesAsync();
        }
        #endregion
        public void Dispose()
        {

        }
    }
}
