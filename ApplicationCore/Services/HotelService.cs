using ApplicationCore.DTOs;
using ApplicationCore.Interfaces;
using AutoMapper;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationCore.Services
{
    public class HotelService : BaseService, IHotelService
    {
        public HotelService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }

        //load data from relative tables(available hotel convs, etc.)
        public HotelDto Get(int hotelId)
        {
            var hotel = unitOfWork.Hotels.Get(h => h.Id == hotelId);
            return mapper.Map<Hotel, HotelDto>(hotel.FirstOrDefault());
        }

        public int GetHotelCount(string searchValue)
        {
            return unitOfWork.Hotels.Get(hc => (hc.Name.Contains(searchValue) || hc.Location.Contains(searchValue))).Count();
        }

        public IEnumerable<HotelDto> GetHotels()
        {
            IEnumerable<Hotel> hotels = unitOfWork.Hotels.GetAll();
            return mapper.Map<IEnumerable<Hotel>, IEnumerable<HotelDto>>(hotels);
        }

        public IEnumerable<HotelDto> GetHotels(int page, int countOnPage, string searchValue)
        {
            IEnumerable<Hotel> hotels = unitOfWork.Hotels.Get(hc =>
            hc.Name.Contains(searchValue) || hc.Location.Contains(searchValue))
                .Skip((page - 1) * countOnPage)
                .Take(countOnPage);

            return mapper.Map<IEnumerable<Hotel>, IEnumerable<HotelDto>>(hotels);
        }

        public void Insert(HotelDto hotel)
        {
            Hotel hotel_to_add = mapper.Map<HotelDto, Hotel>(hotel);
            unitOfWork.Hotels.Insert(hotel_to_add);
            unitOfWork.Save();
        }

        public void Update(HotelDto hotel)
        {
            Hotel hotel_to_update = mapper.Map<HotelDto, Hotel>(hotel);
            unitOfWork.Hotels.Update(hotel_to_update);
            unitOfWork.Save();
        }
        public void Delete(int id)
        {
            Hotel hotel_to_delete = unitOfWork.Hotels.GetById(id);
            unitOfWork.Hotels.Delete(hotel_to_delete);
            unitOfWork.Save();
        }
    }
}
