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
            IAuthenticationManager authenticationManager, IHotelManager hotelManager, IOrderManager orderManager,
            IAdditionalConvManager additionalConvManager)
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
        public IEnumerable<AdminUserDTO> GetUsers(string sortOrder = null, string searchString = null)
        {
            IEnumerable<AdminUserDTO> users = _mapper.Map<List<AppUser>, List<AdminUserDTO>>(_userManager.Users.ToList());
            
            if (!String.IsNullOrEmpty(searchString))
                users = users.Where(u => u.Email.Contains(searchString)
                                    || u.FirstName.Contains(searchString)
                                    || u.LastName.Contains(searchString));
            

            switch (sortOrder)
            {
                case "name_desc":
                    users = users.OrderByDescending(u => u.Email).ToList();
                    break;
                case "first":
                    users = users.OrderBy(u => u.FirstName).ToList();
                    break;
                case "first_desc":
                    users = users.OrderByDescending(u => u.FirstName).ToList();
                    break;
                case "second":
                    users = users.OrderBy(u => u.LastName).ToList();
                    break;
                case "second_desc":
                    users = users.OrderByDescending(u => u.LastName).ToList();
                    break;
                default:
                    users = users.OrderBy(u => u.Email).ToList();
                    break;
            }
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
        public async Task<HotelDTO> GetHotelById(int Id) => await _hotelManager.GetHotelById(Id);
        public IEnumerable<HotelDTO> GetHotels(HotelFilterDto hotelFilterDto ,string sortOrder = null) => _hotelManager.GetHotelsAdmin(hotelFilterDto,sortOrder);
        public async Task<OperationDetails> CreateHotel(HotelDTO hotelDTO) => await _hotelManager.Create(hotelDTO);
        public async Task<OperationDetails> EditHotel(HotelDTO hotelDTO) => await _hotelManager.Update(hotelDTO);
        public async Task DeleteHotel(int Id) => await _hotelManager.Delete(Id);


        public IEnumerable<HotelConvDTO> GetHotelConvs(string sortOrder = null, string searchString=null) => _hotelManager.GetHotelConvs(sortOrder,searchString);
        public Task<OperationDetails> CreateHotelConv(HotelConvDTO hotelConvDTO) => _hotelManager.CreateHotelConv(hotelConvDTO);
        public async Task DeleteHotelConv(int Id) => await _hotelManager.DeleteHotelConv(Id);
        public HotelConvDTO GetHotelConvById(int Id) => _hotelManager.GetHotelConvById(Id);
        public async Task<OperationDetails> EditHotelConv(HotelConvDTO hotelConvDTO) => await _hotelManager.UpdateHotelConv(hotelConvDTO);


        public HotelRoomDTO GetHotelRoomById(int Id) => _hotelManager.GetHotelRoomById(Id);
        public IEnumerable<HotelRoomDTO> GetHotelRooms(string sortOrder = null, string searchString = null) => _hotelManager.GetHotelRooms(sortOrder, searchString);
        public async Task<OperationDetails> CreateHotelRoom(HotelRoomDTO hotelRoomDTO) => await _hotelManager.CreateHotelRoom(hotelRoomDTO);
        public async Task<OperationDetails> EditHotelRoom(HotelRoomDTO hotelRoomDTO) => await _hotelManager.UpdateHotelRoom(hotelRoomDTO);
        public async Task DeleteHotelRoom(int Id) => await _hotelManager.DeleteHotelRoom(Id);

        
        public IEnumerable<HotelRoomConvDTO> GetRoomConvs(int Id, string sortOrder=null, string searchString = null) => _hotelManager.GetHotelRoomConvs(Id, sortOrder, searchString);
        public async Task<OperationDetails> CreateRoomConv(HotelRoomConvDTO roomConv) => await _hotelManager.CreateHotelRoomConv(roomConv);
        public async Task DeleteHotelRoomConv(int Id) => await _hotelManager.DeleteHotelRoomConv(Id);
        #endregion
        #region AddConvs
        public IEnumerable<AdditionalConvDTO> GetAdditionalConvs() => _additionalConvManager.GetConvs();
        public Task<OperationDetails> CreateAdditionalConv(AdditionalConvDTO additionalConvDTO) => _additionalConvManager.Create(additionalConvDTO);
        #endregion

        #region Orders
        public Task<OrderDTO> GetOrderById(int Id) => _orderManager.GetOrderById(Id);
        public List<OrderDTO> GetOrders() => _orderManager.GetOrders();
        public Task<OperationDetails> CreateOrder(OrderDTO orderDTO)=> _orderManager.CreateOrder(orderDTO);
        public Task<OperationDetails> EditOrder(OrderDTO orderDTO) => _orderManager.EditOrder(orderDTO);
        public async Task DeleteOrder(int id) => await _orderManager.DeleteOrder(id);

        public Task<OrderDetailDTO> GetOrderDetailById(int Id) => _orderManager.GetOrderDetailById(Id);
        public List<OrderDetailDTO> GetOrderDetails(int Id) => _orderManager.GetOrderDetails(Id);
        public Task<OperationDetails> CreateOrderDetails(OrderDetailDTO orderDTO) => _orderManager.CreateOrderDetails(orderDTO);
        public Task<OperationDetails> EditOrderDetails(OrderDetailDTO orderDTO) => _orderManager.EditOrderDetails(orderDTO);
        public Task DeleteOrderDetails(int id) => _orderManager.DeleteOrderDetails(id);
        public bool IsHotelExists(string HotelName) => _orderManager.IsHotelExists(HotelName);
        public bool IsRoomExists(int RoomID) => _orderManager.IsRoomExists(RoomID);
        #endregion

        public void Dispose()
        {

        }

        
    }
}
