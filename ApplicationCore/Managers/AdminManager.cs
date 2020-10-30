using ApplicationCore.DTOs;
using ApplicationCore.Infrastructure;
using ApplicationCore.Interfaces;
using Infrastructure.EF;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Managers
{
    public class AdminManager : IAdminManager
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<AppUser> _userManager;
        private IAuthenticationManager _authenticationManager;
        private IHotelManager _hotelManager;
        private IOrderManager _orderManager;
        public AdminManager(ApplicationDbContext applicationDbContext, UserManager<AppUser> userManager, IAuthenticationManager authenticationManager, IHotelManager hotelManager, IOrderManager orderManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
            _authenticationManager = authenticationManager;
            _hotelManager = hotelManager;
            _orderManager = orderManager;
        }
        #region Users
        public List<AppUser> Users()
        {
            return _userManager.Users.ToList();
        }
        public async Task<OperationDetails> CreateUser(UserDTO userDTO)
        {
            return await _authenticationManager.Register(userDTO);
        }
        public async Task<OperationDetails> EditUser(UserDTO userDTO)
        {
            AppUser user = await _userManager.FindByIdAsync(userDTO.Id);
            if (user != null)
            {
                user.Email = userDTO.Email;
                user.PhoneNumber = userDTO.Phone;
                user.FirstName = userDTO.FirstName;
                user.LastName = userDTO.LastName;

                var res = await _userManager.UpdateAsync(user);
                if (res.Errors.Count() > 0)
                    return new OperationDetails(false, res.Errors.FirstOrDefault().ToString(), "");
                await _applicationDbContext.SaveChangesAsync();
                return new OperationDetails(true, "User account has been changed.", "");
            }
            else
            {
                return new OperationDetails(false, "Can`t find the current user", "");
            }
        } 
        public async Task<OperationDetails> ChangePassword(UserDTO userDTO)
        {
            AppUser user = await _userManager.FindByIdAsync(userDTO.Id);
            if (user != null)
            {
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, userDTO.Password);
                var res = await _userManager.UpdateAsync(user);
                if (res.Errors.Count() > 0)
                    return new OperationDetails(false, res.Errors.FirstOrDefault().ToString(), "");
                await _applicationDbContext.SaveChangesAsync();
                return new OperationDetails(true, "Password has been changed.", "");
            }
            else
            {
                return new OperationDetails(false, "Can`t find the current user", "");
            }
        }
        public async Task DeleteUser(string Id)
        {
            AppUser user = await _userManager.FindByIdAsync(Id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
                await _applicationDbContext.SaveChangesAsync();
            }
        }
        #endregion

        #region Hotels
        public List<Hotel> Hotels()
        {
            return _hotelManager.GetHotels();
        }

        public async Task<OperationDetails> CreateHotel(HotelDTO hotelDTO)
        {
            return await _hotelManager.Create(hotelDTO);
        }
        public async Task DeleteHotel(int Id)
        {
            await _hotelManager.Delete(Id);
        }
        #endregion
       
        public void Dispose()
        {

        }

        #region Orders
        public List<Order> Orders()
        {
            return _orderManager.GetOrders();
        }

        public async Task<OperationDetails> CreateOrder(OrderDTO orderDTO)
        {
            return await _orderManager.CreateOrder(orderDTO);
        }

        public async Task DeleteOrder(int id)
        {
            await _orderManager.DeleteOrder(id);
        }
        #endregion
    }
}
