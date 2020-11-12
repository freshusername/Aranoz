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
        private readonly IOrderManager _orderManager;
        private IAdditionalConvManager _additionalConvManager;
        private readonly IConvsManager _convsManager;
        private readonly IAdminRoomManager _roomManager;
        public AdminManager(ApplicationDbContext applicationDbContext, UserManager<AppUser> userManager,IMapper mapper, 
            IAuthenticationManager authenticationManager, IHotelManager hotelManager, IOrderManager orderManager,
            IAdditionalConvManager additionalConvManager,IConvsManager convsManager,IAdminRoomManager roomManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
            _mapper = mapper;
            _authenticationManager = authenticationManager;
            _hotelManager = hotelManager;
            _orderManager = orderManager;
            _additionalConvManager = additionalConvManager;
            _convsManager = convsManager;
            _roomManager = roomManager;
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
            HotelDTO hotel = await _hotelManager.GetHotelById(Id);
            return hotel;
        }
        public IEnumerable<HotelDTO> Hotels()
        {
            IEnumerable<HotelDTO> hotels =_hotelManager.GetHotels(new FilterHotelDto() );
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

        public async Task DeleteHotelConv(int Id)
        {
            await _hotelManager.DeleteHotelConv(Id);
        }
        #endregion
        #region AddConvs
        public Task<OperationDetails> CreateAdditionalConv(AdditionalConvDTO additionalConvDTO) => _additionalConvManager.Create(additionalConvDTO);
        #endregion

        #region Orders
        public AdminOrderDTO GetOrderById(int Id) => _orderManager.GetOrderById(Id);
        public List<AdminOrderDTO> GetOrders() => _orderManager.GetOrders();
        public Task<OperationDetails> CreateOrder(AdminOrderDTO orderDTO)=> _orderManager.CreateOrder(orderDTO);
        public Task<OperationDetails> EditOrder(AdminOrderDTO orderDTO) => _orderManager.EditOrder(orderDTO);
        public async Task DeleteOrder(int id) => await _orderManager.DeleteOrder(id);

        public AdminOrderDetailDTO GetOrderDetailById(int Id) => _orderManager.GetOrderDetailById(Id);
        public List<AdminOrderDetailDTO> GetOrderDetails(int Id) => _orderManager.GetOrderDetails(Id);
        public Task<OperationDetails> CreateOrderDetails(AdminOrderDetailDTO orderDTO) => _orderManager.CreateOrderDetails(orderDTO);
        public Task<OperationDetails> EditOrderDetails(AdminOrderDetailDTO orderDTO) => _orderManager.EditOrderDetails(orderDTO);
        public Task DeleteOrderDetails(int id) => _orderManager.DeleteOrderDetails(id);
        public bool IsHotelExists(string HotelName) => _orderManager.IsHotelExists(HotelName);
        public bool IsRoomExists(int RoomID) => _orderManager.IsRoomExists(RoomID);
        #endregion


        #region Convs
        public List<AdditionalConvDTO> GetConvs() => _convsManager.GetConvs();
        public AdditionalConvDTO GetConvById(int Id) => _convsManager.GetConvById(Id);
        public Task<OperationDetails> CreateConv(AdditionalConvDTO convDTO) => _convsManager.CreateConv(convDTO);
        public Task<OperationDetails> EditConv(AdditionalConvDTO convDTO) => _convsManager.EditConv(convDTO);
        public Task DeleteConv(int Id) => _convsManager.DeleteConv(Id);
        #endregion

        #region Rooms
        public List<AdminRoomDTO> GetRooms() => _roomManager.GetRooms();
        public AdminRoomDTO GetRoomById(int Id) => _roomManager.GetRoomById(Id);
        public Task<OperationDetails> CreateRoom(AdminRoomDTO convDTO) => _roomManager.CreateRoom(convDTO);
        public Task<OperationDetails> EditRoom(AdminRoomDTO convDTO) => _roomManager.EditRoom(convDTO);
        public Task DeleteRoom(int Id) => _roomManager.DeleteRoom(Id);
        #endregion
        public void Dispose()
        {

        }
    }
}
