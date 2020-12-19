using ApplicationCore.DTOs;
using ApplicationCore.Infrastructure;
using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAdminManager : IDisposable
    {
        #region Users
        IEnumerable<AdminUserDTO> GetUsers(AdminPaginationDTO paginationDTO, string sortOrder);
        Task<OperationDetails> CreateUser(UserDTO userDTO);
        Task<OperationDetails> EditUser(UserDTO userDTO);
        Task<OperationDetails> ChangePassword(UserDTO userDTO);
        Task DeleteUser(string id);
        #endregion

        #region Hotels
        Task<HotelDTO> GetHotelById(int Id);
        IEnumerable<HotelDTO> GetHotels(AdminPaginationDTO paginationDTO, string sortOrder);
        Task<OperationDetails> CreateHotel(HotelDTO hotelDTO);
        Task<OperationDetails> EditHotel(HotelDTO hotelDTO);
        Task DeleteHotel(int Id);

        IEnumerable<HotelConvDTO> GetHotelConvs(AdminPaginationDTO paginationDTO, string sortOrder);
        Task<OperationDetails> CreateHotelConv(HotelConvDTO hotelConvDTO);
        Task DeleteHotelConv(int Id);
        HotelConvDTO GetHotelConvById(int Id);
        Task<OperationDetails> EditHotelConv(HotelConvDTO hotelConvDTO);

        HotelRoomDTO GetHotelRoomById(int Id);
        IEnumerable<HotelRoomDTO> GetHotelRooms(AdminPaginationDTO paginationDTO, string sortOrder);
        Task<OperationDetails> CreateHotelRoom(HotelRoomDTO hotelRoomDTO);
        Task<OperationDetails> EditHotelRoom(HotelRoomDTO hotelRoomDTO);
        Task DeleteHotelRoom(int Id);

        IEnumerable<HotelRoomConvDTO> GetRoomConvs(int Id, AdminPaginationDTO paginationDTO, string sortOrder);
        Task<OperationDetails> CreateRoomConv(HotelRoomConvDTO roomConv);
        Task DeleteHotelRoomConv(int Id);
        #endregion
        #region AddConv
        IEnumerable<AdditionalConvDTO> GetAdditionalConvs();
        Task<OperationDetails> CreateAdditionalConv(AdditionalConvDTO additionalConvDTO);

        #endregion
        #region Orders
        AdminOrderDTO GetOrderById(int Id);
        List<AdminOrderDTO> GetOrders();
        Task<OperationDetails> CreateOrder(AdminOrderDTO orderDTO);
        Task<OperationDetails> EditOrder(AdminOrderDTO orderDTO);
        Task DeleteOrder(int id);

        AdminOrderDetailDTO GetOrderDetailById(int Id);
        List<AdminOrderDetailDTO> GetOrderDetails(int Id);
        Task<OperationDetails> CreateOrderDetails(AdminOrderDetailDTO orderDTO);
        Task<OperationDetails> EditOrderDetails(AdminOrderDetailDTO orderDTO);
        bool IsHotelExists(string HotelName);
        bool IsRoomExists(int RoomID);
        Task DeleteOrderDetails(int id);
        #endregion

        #region Convs
        List<AdditionalConvDTO> GetConvs();
        AdditionalConvDTO GetConvById(int Id);
        Task<OperationDetails> CreateConv(AdditionalConvDTO convDTO);
        Task<OperationDetails> EditConv(AdditionalConvDTO convDTO);
        Task DeleteConv(int Id);
        #endregion

        #region Rooms
        List<AdminRoomDTO> GetRooms();
        AdminRoomDTO GetRoomById(int Id);
        Task<OperationDetails> CreateRoom(AdminRoomDTO convDTO);
        Task<OperationDetails> EditRoom(AdminRoomDTO convDTO);
        Task DeleteRoom(int Id);
        #endregion
    }
}
