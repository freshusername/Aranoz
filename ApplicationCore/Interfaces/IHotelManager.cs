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
        IEnumerable<HotelDTO> GetHotels(HotelFilterDTO filterHotelDto);
        IEnumerable<HotelDTO> GetHotelsAdmin(AdminPaginationDTO paginationDTO, string sortOrder);
        Task<OperationDetails> Create(HotelDTO hotelDTO);
        Task<OperationDetails> Update(HotelDTO hotelDTO);
        Task Delete(int Id);

        IEnumerable<HotelConvDTO> GetHotelConvs(AdminPaginationDTO paginationDTO, string sortOrder);
        IEnumerable<HotelConvDTO> GetHotelConvs();
        IEnumerable<AdditionalConvDTO> GetRoomConvs();
        
        Task<OperationDetails> CreateHotelConv(HotelConvDTO hotelConvDTO);
        Task DeleteHotelConv(int Id);
        HotelConvDTO GetHotelConvById(int Id);
        Task<OperationDetails> UpdateHotelConv(HotelConvDTO hotelConvDTO);

        HotelRoomDTO GetHotelRoomById(int Id);
        IEnumerable<HotelRoomDTO> GetHotelRooms(AdminPaginationDTO paginationDTO, string sortOrder);
        Task<OperationDetails> CreateHotelRoom(HotelRoomDTO hotelRoomDTO);
        Task<OperationDetails> UpdateHotelRoom(HotelRoomDTO hotelRoomDTO);
        Task DeleteHotelRoom(int Id);

        IEnumerable<HotelRoomConvDTO> GetHotelRoomConvs(int Id, AdminPaginationDTO paginationDTO, string sortOrder);
        Task<OperationDetails> CreateHotelRoomConv(HotelRoomConvDTO conv);
        Task DeleteHotelRoomConv(int Id);
    }
}
