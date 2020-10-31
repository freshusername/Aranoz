using ApplicationCore.DTOs;
using ApplicationCore.Infrastructure;
using ApplicationCore.Interfaces;
using AutoMapper;
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
        private IMapper _mapper;
        private IAuthenticationManager _authenticationManager;
        private IHotelManager _hotelManager;
        private IOrderManager _orderManager;
        private IAdditionalConvManager _additionalConvManager;
        public AdminManager(ApplicationDbContext applicationDbContext, UserManager<AppUser> userManager,IMapper mapper, 
            IAuthenticationManager authenticationManager, IHotelManager hotelManager, IOrderManager orderManager, IAdditionalConvManager additionalConvManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
            _mapper = mapper;
            _authenticationManager = authenticationManager;
            _hotelManager = hotelManager;
            _orderManager = orderManager;
            _additionalConvManager = additionalConvManager;
        }
        #region Users
        public List<AdminUserDTO> Users()
        {
            List<AdminUserDTO> users = _mapper.Map<List<AppUser>, List<AdminUserDTO>>(_userManager.Users.ToList());
            return users;
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
        public async Task<HotelDTO> GetHotelById(int Id)
        {
            HotelDTO hotel = _mapper.Map<Hotel, HotelDTO>(await _hotelManager.GetHotelById(Id));
            return hotel;
        }
        public List<HotelDTO> Hotels()
        {
            List<HotelDTO> hotels =_mapper.Map<List<Hotel>,List<HotelDTO>>(_hotelManager.GetHotels());
            return hotels;
        }

        public async Task<OperationDetails> CreateHotel(HotelDTO hotelDTO)
        {
            return await _hotelManager.Create(hotelDTO);
        }
        public async Task<OperationDetails> EditHotel(HotelDTO hotelDTO)
        {
            return await _hotelManager.Update(hotelDTO);
        }
        public async Task DeleteHotel(int Id)
        {
            await _hotelManager.Delete(Id);
        }
        public IEnumerable<HotelConvDTO> HotelConvs() => _hotelManager.GetHotelConvs();

        public Task<OperationDetails> CreateHotelConv(HotelConvDTO hotelConvDTO) => _hotelManager.CreateHotelConv(hotelConvDTO);
        #endregion
        #region AddConvs
        public Task<OperationDetails> CreateAdditionalConv(AdditionalConvDTO additionalConvDTO) => _additionalConvManager.Create(additionalConvDTO);
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
