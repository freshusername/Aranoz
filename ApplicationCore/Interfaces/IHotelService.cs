using ApplicationCore.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public interface IHotelService
    {
        HotelDto Get(int id);
        IEnumerable<HotelDto> GetHotels();
        IEnumerable<HotelDto> GetHotels(int page, int countOnPage, string searchValue);
        int GetHotelCount(string searchValue);

        void Insert(HotelDto item);
        void Update(HotelDto item);
        void Delete(int id);

    }
}
