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
        List<AdminUserDTO> Users();
        Task<OperationDetails> CreateUser(UserDTO userDTO);
        Task<OperationDetails> EditUser(UserDTO userDTO);
        Task<OperationDetails> ChangePassword(UserDTO userDTO);
        Task DeleteUser(string id);
        #endregion

        #region Hotels
        Task<HotelDTO> GetHotelById(int Id);
        IEnumerable<HotelDTO> Hotels();
        Task<OperationDetails> CreateHotel(HotelDTO hotelDTO);
        Task<OperationDetails> EditHotel(HotelDTO hotelDTO);
        Task DeleteHotel(int Id);
        IEnumerable<HotelConvDTO> HotelConvs();
        Task<OperationDetails> CreateHotelConv(HotelConvDTO hotelConvDTO);
        Task DeleteHotelConv(int Id);
        #endregion
        #region AddConv
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
