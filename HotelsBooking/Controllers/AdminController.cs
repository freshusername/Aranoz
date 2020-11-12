using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.DTOs;
using ApplicationCore.Interfaces;
using AutoMapper;
using HotelsBooking.Models;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static Infrastructure.Enums;

namespace HotelsBooking.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminManager _adminManager;
        private readonly UserManager<AppUser> _userManager;
        private IMapper _mapper;
        public AdminController(IAdminManager adminManager, UserManager<AppUser> userManager, IMapper mapper)
        {
            _adminManager = adminManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        #region Users
        public IActionResult Users()
        {
            List<UsersViewModel> users = _mapper.Map<List<AdminUserDTO>, List<UsersViewModel>>(_adminManager.Users());
            return View(users);
        }

        public async Task<IActionResult> EditUser(string Id)
        {
            AppUser user = await _userManager.FindByIdAsync(Id);
            if (user == null)
            {
                return NotFound();
            }
            EditUserViewModel model = _mapper.Map<AppUser, EditUserViewModel>(user);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = _mapper.Map<EditUserViewModel, UserDTO>(model);
            var res = await _adminManager.EditUser(user);
            if (res.Succedeed)
                return RedirectToAction("Users");
            else
                ModelState.AddModelError(res.Property, res.Message);
            
            return View(model);
        }

        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _mapper.Map<RegisterViewModel, UserDTO>(model);
            var result = await _adminManager.CreateUser(user);
            if (result.Succedeed)
                return RedirectToAction("Users");
            else
                ModelState.AddModelError(result.Property, result.Message);

            return View(model);
        }

        public async Task<IActionResult> ChangePassword(string Id)
        {
            AppUser user = await _userManager.FindByIdAsync(Id);
            if (user == null)
            {
                return NotFound();
            }
            ChangePasswordViewModel model = _mapper.Map<AppUser,ChangePasswordViewModel>(user);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = _mapper.Map<ChangePasswordViewModel, UserDTO>(model);
            var res = await _adminManager.ChangePassword(user);
            if (res.Succedeed)
                return RedirectToAction("Users");
            else
                ModelState.AddModelError(res.Property, res.Message);
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string Id)
        {
            await _adminManager.DeleteUser(Id);
            return RedirectToAction("Users");
  
        }
        #endregion
        #region Hotels
        public IActionResult Hotels()
        {
            IEnumerable<CreateOrEditHotelViewModel> hotels = _mapper.Map<IEnumerable<HotelDTO>, IEnumerable<CreateOrEditHotelViewModel>>(_adminManager.Hotels());
            return View(hotels);
        }
        public IActionResult CreateHotel()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateHotel(CreateOrEditHotelViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var hotel = _mapper.Map<CreateOrEditHotelViewModel, HotelDTO>(model);
            var res = await _adminManager.CreateHotel(hotel);
            if (res.Succedeed)
                return RedirectToAction("Hotels");
            else
                ModelState.AddModelError(res.Property, res.Message);

            return View(model);
        }

        public async Task<IActionResult> EditHotel(int Id)
        {
            CreateOrEditHotelViewModel hotel = _mapper.Map<HotelDTO, CreateOrEditHotelViewModel>(await _adminManager.GetHotelById(Id));
            if (hotel == null)
            {
                return NotFound();
            }
            return View(hotel);
        }

        [HttpPost]
        public async Task<IActionResult> EditHotel(CreateOrEditHotelViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            HotelDTO hotel = _mapper.Map<CreateOrEditHotelViewModel, HotelDTO>(model);
            var res = await _adminManager.EditHotel(hotel);
            if (res.Succedeed)
            {
                return RedirectToAction("Hotels");
            }
            else
            {
                ModelState.AddModelError(res.Property, res.Message);
            }
            return View(model);
        }

        public async Task<IActionResult> HotelConvs(int Id)
        {
            IEnumerable<HotelConvsViewModel> hotelConvs = _mapper.Map<IEnumerable<HotelConvDTO>, IEnumerable<HotelConvsViewModel>>(_adminManager.HotelConvs().Where(hc => hc.HotelId == Id));
            return View(hotelConvs);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteHotelConv(int Id)
        {
            await _adminManager.DeleteHotelConv(Id);
            int HotelId = Id;
            return RedirectToAction("HotelConvs", new { Id = HotelId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteHotel(int Id)
        {
            await _adminManager.DeleteHotel(Id);
            return RedirectToAction("Hotels");
        }
        #endregion
        #region Order
        public IActionResult Orders()
        {
            return View(_mapper.Map<List<OrderDTO>, List<OrdersViewModel>>(_adminManager.GetOrders()));
        }

        public IActionResult CreateOrder() => View();

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrEditOrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                View(model);
            }
            AppUser appUser = _userManager.Users
                .FirstOrDefault(p => p.FirstName == model.FirstName && p.LastName == model.LastName);

            if (appUser == null)
            {
                ModelState.AddModelError("User", "User is not exist");
                View(model);
            }
            OrderDTO orderDTO = _mapper.Map<CreateOrEditOrderViewModel, OrderDTO>(model);

            var result = await _adminManager.CreateOrder(orderDTO);
            if (result.Succedeed)
                return RedirectToAction("Orders");
            else
                ModelState.AddModelError(result.Property, result.Message);
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> EditOrder(int Id)
        {
            OrderDTO orderDTO = await _adminManager.GetOrderById(Id);
            if (orderDTO == null)
            {
                return NotFound();
            }
            CreateOrEditOrderViewModel model = _mapper.Map<OrderDTO, CreateOrEditOrderViewModel>(orderDTO);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditOrder(CreateOrEditOrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            OrderDTO orderDTO = _mapper.Map<CreateOrEditOrderViewModel, OrderDTO>(model);
            var res = await _adminManager.EditOrder(orderDTO);
            if (res.Succedeed)
                return RedirectToAction("Orders");
            else
                ModelState.AddModelError(res.Property, res.Message);

            return View(model);
        }

        public async Task<IActionResult> DeleteOrder(int Id)
        {
            if (_adminManager.GetOrderById(Id) != null)
                await _adminManager.DeleteOrder(Id);
            return RedirectToAction("Orders");
        }



        public IActionResult OrderDetails(int id)
        {
            return View(_mapper.Map<List<OrderDetailDTO>, List<OrderDetailsViewModel>>(_adminManager.GetOrderDetails(id)));
        }


        public IActionResult CreateOrderDetails() => View();

        [HttpPost]
        public async Task<IActionResult> CreateOrderDetails(CreateOrEditOrderDetailsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                View(model);
            }
            if (!_adminManager.IsHotelExists(model.HotelName))
            {
                ModelState.AddModelError("HotelName", "Hotel is not exist");
                View(model);
            }

            if (!_adminManager.IsRoomExists(model.RoomId))
            {
                ModelState.AddModelError("RoomID", "Room is not exist");
                View(model);
            }

            OrderDetailDTO orderDTO = _mapper.Map<CreateOrEditOrderDetailsViewModel, OrderDetailDTO>(model);

            var result = await _adminManager.CreateOrderDetails(orderDTO);
            if (result.Succedeed)
                return RedirectToAction("OrderDetails");
            else
                ModelState.AddModelError(result.Property, result.Message);
            return View(model);
        }

        public async Task<IActionResult> EditOrderDetails(int id)
        {
            OrderDetailDTO order = await _adminManager.GetOrderDetailById(id);
            if (order == null)
            {
                return NotFound();
            }
            CreateOrEditOrderDetailsViewModel model = _mapper.Map<OrderDetailDTO, CreateOrEditOrderDetailsViewModel>(order);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditOrderDetails(CreateOrEditOrderDetailsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            OrderDetailDTO orderDTO = _mapper.Map<CreateOrEditOrderDetailsViewModel, OrderDetailDTO>(model);
            var res = await _adminManager.EditOrderDetails(orderDTO);
            if (res.Succedeed)
                return RedirectToAction("OrderDetails");
            else
                ModelState.AddModelError(res.Property, res.Message);
            return View(model);
        }
        public async Task<IActionResult> DeleteOrderDetails(int Id)
        {
            if (_adminManager.GetOrderDetailById(Id) != null)
                await _adminManager.DeleteOrderDetails(Id);
            return RedirectToAction("OrderDetails");
        }
        #endregion
    }

}