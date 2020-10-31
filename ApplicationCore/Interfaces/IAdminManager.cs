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
        List<HotelDTO> Hotels();
        Task<OperationDetails> CreateHotel(HotelDTO hotelDTO);
        Task<OperationDetails> EditHotel(HotelDTO hotelDTO);
        Task DeleteHotel(int Id);
        IEnumerable<HotelConvDTO> HotelConvs();
        Task<OperationDetails> CreateHotelConv(HotelConvDTO hotelConvDTO);
        #endregion
        #region AddConv
        Task<OperationDetails> CreateAdditionalConv(AdditionalConvDTO additionalConvDTO);
        #endregion
        #region Orders
        List<Order> Orders();
        Task<OperationDetails> CreateOrder(OrderDTO orderDTO);
        Task DeleteOrder(int id);
        #endregion

    }
}
