using ApplicationCore.DTOs;
using ApplicationCore.Infrastructure;
using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IHotelManager : IDisposable
    {
        Task<HotelDTO> GetHotelById(int Id);
        Task<HotelDTO> GetHotelDetails(FilterHotelDetailDTO filterHotelDetailDTO);
        IEnumerable<HotelDTO> GetHotels(HotelFilterDto filterHotelDto);
        IEnumerable<HotelDTO> GetHotelsAdmin(string sortOrder, string searchString);
        Task<OperationDetails> Create(HotelDTO hotelDTO);
        Task<OperationDetails> Update(HotelDTO hotelDTO);
        Task Delete(int Id);
        IEnumerable<HotelConvDTO> GetHotelConvs();
        IEnumerable<AdditionalConvDTO> GetRoomConvs();
        IEnumerable<HotelConvDTO> GetHotelConvs(string sortOrder, string searchString);
        Task<OperationDetails> CreateHotelConv(HotelConvDTO hotelConvDTO);
        Task DeleteHotelConv(int Id);
        HotelConvDTO GetHotelConvById(int Id);
        Task<OperationDetails> UpdateHotelConv(HotelConvDTO hotelConvDTO);

        HotelRoomDTO GetHotelRoomById(int Id);
        IEnumerable<HotelRoomDTO> GetHotelRooms(string sortOrder, string searchString);
        Task<OperationDetails> CreateHotelRoom(HotelRoomDTO hotelRoomDTO);
        Task<OperationDetails> UpdateHotelRoom(HotelRoomDTO hotelRoomDTO);
        Task DeleteHotelRoom(int Id);

        IEnumerable<HotelRoomConvDTO> GetHotelRoomConvs(int Id, string sortOrder, string searchString);
        Task<OperationDetails> CreateHotelRoomConv(HotelRoomConvDTO conv);
        Task DeleteHotelRoomConv(int Id);
    }
}
