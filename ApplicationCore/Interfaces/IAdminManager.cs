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
        List<AppUser> Users();
        Task<OperationDetails> CreateUser(UserDTO userDTO);
        Task<OperationDetails> EditUser(UserDTO userDTO);
        Task<OperationDetails> ChangePassword(UserDTO userDTO);
        Task DeleteUser(string id);
        #endregion

        #region Hotels
        List<Hotel> Hotels();
        Task<OperationDetails> CreateHotel(HotelDTO hotelDTO);
        Task DeleteHotel(int Id);
        #endregion

        #region Orders
        List<Order> Orders();
        Task<OperationDetails> CreateOrder(OrderDTO orderDTO);
        Task DeleteOrder(int id);
        #endregion

    }
}
