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
            return View(_mapper.Map<List<AdminOrderDTO>, List<OrdersViewModel>>(_adminManager.GetOrders()));
        }

        public IActionResult CreateOrder() => View();

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrEditOrderViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            AppUser appUser = _userManager.Users.FirstOrDefault(p => p.FirstName == model.FirstName && p.LastName == model.LastName);
            if (appUser == null)
            {
                ModelState.AddModelError("User", "User is not found");
                return View(model);
            }
            AdminOrderDTO orderDTO = _mapper.Map<CreateOrEditOrderViewModel, AdminOrderDTO>(model);
            await _adminManager.CreateOrder(orderDTO);
            return RedirectToAction("Orders");
        }

        
        public async Task<IActionResult> EditOrder(int Id)
        {
            AdminOrderDTO orderDTO = _adminManager.GetOrderById(Id);
            if (orderDTO == null)
            {
                return NotFound();
            }

            CreateOrEditOrderViewModel model = _mapper.Map<AdminOrderDTO, CreateOrEditOrderViewModel>(orderDTO);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditOrder(CreateOrEditOrderViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            AppUser appUser = _userManager.Users.FirstOrDefault(p => p.FirstName == model.FirstName && p.LastName == model.LastName);
            if (appUser == null)
            {
                ModelState.AddModelError("User", "User is not found");
                return View(model);
            }
            AdminOrderDTO orderDTO = _mapper.Map<CreateOrEditOrderViewModel, AdminOrderDTO>(model);
            var res = await _adminManager.EditOrder(orderDTO);
            if (res.Succedeed)
                return RedirectToAction("Orders");
            else
            {
                ModelState.AddModelError(res.Property, res.Message);
                return View(model);
            }
        }

        public async Task<IActionResult> DeleteOrder(int Id)
        {
            if (_adminManager.GetOrderById(Id) != null)
                await _adminManager.DeleteOrder(Id);
            return RedirectToAction("Orders");
        }



        public IActionResult OrderDetails(int id)
        {
            ViewBag.OrderID = id;
            return View(_mapper.Map<List<AdminOrderDetailDTO>, List<OrderDetailsViewModel>>(_adminManager.GetOrderDetails(id)));
        }


        public IActionResult CreateOrderDetails(int id)
        {
            CreateOrEditOrderDetailsViewModel model = new CreateOrEditOrderDetailsViewModel
            {
                OrderID = id,
                CheckInDate = DateTimeOffset.Now,
                CheckOutDate= DateTimeOffset.Now
            };
            return View(model);
        }

        public IActionResult IsRoomExists(int Id) => Json(_adminManager.IsRoomExists(Id));
        public IActionResult IsHotelExists(string HotelName) => Json(_adminManager.IsHotelExists(HotelName));

        [HttpPost]
        public async Task<IActionResult> CreateOrderDetails(CreateOrEditOrderDetailsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                View(model);
            }
            AdminOrderDTO adminOrderDTO= _adminManager.GetOrderById(model.OrderID);
            if (model.OrderID != adminOrderDTO.Id)
            {
                ModelState.AddModelError("OrderID", "Order is not found!");
                return View(model);
            }
            if (!_adminManager.IsHotelExists(model.HotelName))
            {
                ModelState.AddModelError("HotelName", "The hotel is not exist");
                return View(model);
            }

            if (!_adminManager.IsRoomExists(model.RoomId))
            {
                ModelState.AddModelError("RoomID", "The room is not exist");
                return View(model);
            }

            AdminOrderDetailDTO orderDTO = _mapper.Map<CreateOrEditOrderDetailsViewModel, AdminOrderDetailDTO>(model);

            var result = await _adminManager.CreateOrderDetails(orderDTO);
            if (result.Succedeed)
            {
                ViewBag.OrderID = model.OrderID;
                return View("OrderDetails", _mapper.Map<List<AdminOrderDetailDTO>, List<OrderDetailsViewModel>>(_adminManager.GetOrderDetails(model.OrderID)));
            }
            else
                ModelState.AddModelError(result.Property, result.Message);
            return View(model);
        }

        public IActionResult EditOrderDetails(int id,int OrderID)
        {
            AdminOrderDetailDTO order = _adminManager.GetOrderDetailById(id);
            if (order == null)
            {
                return NotFound();
            }
            CreateOrEditOrderDetailsViewModel model = _mapper.Map<AdminOrderDetailDTO, CreateOrEditOrderDetailsViewModel>(order);
            model.OrderID = OrderID;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditOrderDetails(CreateOrEditOrderDetailsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            AdminOrderDetailDTO orderDTO = _mapper.Map<CreateOrEditOrderDetailsViewModel, AdminOrderDetailDTO>(model);
            if (!_adminManager.IsHotelExists(model.HotelName))
            {
                ModelState.AddModelError("HotelName", "The hotel is not exist");
                return View(model);
            }

            if (!_adminManager.IsRoomExists(model.RoomId))
            {
                ModelState.AddModelError("RoomID", "The room is not exist");
                return View(model);
            }
            var res = await _adminManager.EditOrderDetails(orderDTO);
            if (res.Succedeed)
            {
                ViewBag.OrderID = model.OrderID;
                return View("OrderDetails", _mapper.Map<List<AdminOrderDetailDTO>, List<OrderDetailsViewModel>>(_adminManager.GetOrderDetails(model.OrderID)));
            }   
            else
                ModelState.AddModelError(res.Property, res.Message);
            return View(model);
        }
        public async Task<IActionResult> DeleteOrderDetails(int Id,int OrderID)
        {
            if (_adminManager.GetOrderDetailById(Id) != null)
                await _adminManager.DeleteOrderDetails(Id);
            var model = _mapper.Map<List<AdminOrderDetailDTO>, List<OrderDetailsViewModel>>(_adminManager.GetOrderDetails(OrderID));
            ViewBag.OrderID = OrderID;
            return View("OrderDetails", model);
        }
        #endregion
        #region Convs
        public IActionResult Convs()
        {
            return View(_mapper.Map<List<AdditionalConvDTO>, List<ConvsViewModel>>
                        (_adminManager.GetConvs()));
        }

        public IActionResult CreateConv()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateConv(ConvsViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            AdditionalConvDTO conv = _mapper.Map<ConvsViewModel, AdditionalConvDTO>(model);
            var res = await _adminManager.CreateConv(conv);
            if (res.Succedeed)
            {
                return RedirectToAction("Convs");
            }
            else
            {
                ModelState.AddModelError(res.Property, res.Message);
                return View(model);
            }
        }

        public IActionResult EditConv(int Id)
        {
            AdditionalConvDTO addConv = _adminManager.GetConvById(Id);
            if (addConv == null)
                return NotFound();
            else
                return View(_mapper.Map<AdditionalConvDTO,ConvsViewModel>(addConv));
        }

        [HttpPost]
        public async Task<IActionResult> EditConv(ConvsViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            AdditionalConvDTO conv = _mapper.Map<ConvsViewModel, AdditionalConvDTO>(model);
            var res = await _adminManager.EditConv(conv);
            if (res.Succedeed)
            {
                return RedirectToAction("Convs");
            }
            else
            {
                ModelState.AddModelError(res.Property, res.Message);
                return View(model);
            }
        }

        public async Task<IActionResult> DeleteConv(int Id)
        {
            if (_adminManager.GetConvById(Id) != null)
                await _adminManager.DeleteConv(Id);
            return RedirectToAction("Convs");
        }
        #endregion
        #region Rooms
        public IActionResult Rooms()
        {
            return View(_mapper.Map<List<AdminRoomDTO>, List<AdminRoomsViewModel>>
                        (_adminManager.GetRooms()));
        }

        public IActionResult CreateRoom()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom(AdminRoomsViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            AdminRoomDTO conv = _mapper.Map<AdminRoomsViewModel, AdminRoomDTO>(model);
            var res = await _adminManager.CreateRoom(conv);
            if (res.Succedeed)
            {
                return RedirectToAction("Rooms");
            }
            else
            {
                ModelState.AddModelError(res.Property, res.Message);
                return View(model);
            }
        }

        public IActionResult EditRoom(int Id)
        {
            AdminRoomDTO addConv = _adminManager.GetRoomById(Id);
            if (addConv == null)
                return NotFound();
            else
                return View(_mapper.Map<AdminRoomDTO, AdminRoomsViewModel>(addConv));
        }

        [HttpPost]
        public async Task<IActionResult> EditRoom(AdminRoomsViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            AdminRoomDTO conv = _mapper.Map<AdminRoomsViewModel, AdminRoomDTO>(model);
            var res = await _adminManager.EditRoom(conv);
            if (res.Succedeed)
            {
                return RedirectToAction("Rooms");
            }
            else
            {
                ModelState.AddModelError(res.Property, res.Message);
                return View(model);
            }
        }

        public async Task<IActionResult> DeleteRoom(int Id)
        {
            if (_adminManager.GetRoomById(Id) != null)
                await _adminManager.DeleteRoom(Id);
            return RedirectToAction("Rooms");
        }
        #endregion
    }
}