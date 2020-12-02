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
        IEnumerable<AdminUserDTO> GetUsers(string sortOrder, string searchString);
        Task<OperationDetails> CreateUser(UserDTO userDTO);
        Task<OperationDetails> EditUser(UserDTO userDTO);
        Task<OperationDetails> ChangePassword(UserDTO userDTO);
        Task DeleteUser(string id);
        #endregion

        #region Hotels
        Task<HotelDTO> GetHotelById(int Id);
        IEnumerable<HotelDTO> GetHotels(HotelFilterDto hotelFilterDto,string sortOrder);
        Task<OperationDetails> CreateHotel(HotelDTO hotelDTO);
        Task<OperationDetails> EditHotel(HotelDTO hotelDTO);
        Task DeleteHotel(int Id);

        IEnumerable<HotelConvDTO> GetHotelConvs(string sortOrder,string searchString);
        Task<OperationDetails> CreateHotelConv(HotelConvDTO hotelConvDTO);
        Task DeleteHotelConv(int Id);
        HotelConvDTO GetHotelConvById(int Id);
        Task<OperationDetails> EditHotelConv(HotelConvDTO hotelConvDTO);

        HotelRoomDTO GetHotelRoomById(int Id);
        IEnumerable<HotelRoomDTO> GetHotelRooms(string sortOrder, string searchString);
        Task<OperationDetails> CreateHotelRoom(HotelRoomDTO hotelRoomDTO);
        Task<OperationDetails> EditHotelRoom(HotelRoomDTO hotelRoomDTO);
        Task DeleteHotelRoom(int Id);

        IEnumerable<HotelRoomConvDTO> GetRoomConvs(int Id, string sortOrder, string searchString);
        Task<OperationDetails> CreateRoomConv(HotelRoomConvDTO roomConv);
        Task DeleteHotelRoomConv(int Id);
        #endregion
        #region AddConv
        IEnumerable<AdditionalConvDTO> GetAdditionalConvs();
        Task<OperationDetails> CreateAdditionalConv(AdditionalConvDTO additionalConvDTO);

        #endregion

        #region Orders
        Task<OrderDTO> GetOrderById(int Id);
        List<OrderDTO> GetOrders();
        Task<OperationDetails> CreateOrder(OrderDTO orderDTO);
        Task<OperationDetails> EditOrder(OrderDTO orderDTO);
        Task DeleteOrder(int id);

        Task<OrderDetailDTO> GetOrderDetailById(int Id);
        List<OrderDetailDTO> GetOrderDetails(int Id);
        Task<OperationDetails> CreateOrderDetails(OrderDetailDTO orderDTO);
        Task<OperationDetails> EditOrderDetails(OrderDetailDTO orderDTO);
        bool IsHotelExists(string HotelName);
        bool IsRoomExists(int RoomID);
        Task DeleteOrderDetails(int id);
        #endregion

    }
}
