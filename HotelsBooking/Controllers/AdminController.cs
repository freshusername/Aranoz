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
        private readonly IPhotoManager _photoManager;
        private readonly UserManager<AppUser> _userManager;
        private IMapper _mapper;
        public AdminController(IAdminManager adminManager, IPhotoManager photoManager, UserManager<AppUser> userManager, IMapper mapper)
        {
            _adminManager = adminManager;
            _photoManager = photoManager;
            _userManager = userManager;
            _mapper = mapper;
        }


        #region Users

        [HttpGet]
        public IActionResult Users(AdminPaginationDTO paginationDTO, string sortOrder)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["FirstNameSortParm"] = sortOrder == "first" ? "first_desc" : "first";
            ViewData["SecondNameSortParm"] = sortOrder == "second" ? "second_desc" : "second";
            ViewData["CurrentFilter"] = paginationDTO.KeyWord;

            IEnumerable<UsersViewModel> users = _mapper.Map<IEnumerable<AdminUserDTO>, IEnumerable<UsersViewModel>>(_adminManager.GetUsers(paginationDTO, sortOrder));

            IEnumerableUsersViewModel model = new IEnumerableUsersViewModel
            {
                users = users,
                PaginationDTO = paginationDTO
            };

            return View(model);
        }

        [HttpGet]
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

        [HttpGet]
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

        [HttpGet]
        public async Task<IActionResult> ChangePassword(string Id)
        {
            AppUser user = await _userManager.FindByIdAsync(Id);
            if (user == null)
            {
                return NotFound();
            }
            ChangePasswordViewModel model = _mapper.Map<AppUser, ChangePasswordViewModel>(user);
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

        [HttpGet]
        public IActionResult Hotels(AdminPaginationDTO paginationDTO, string sortOrder = null)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["LocationSortParm"] = sortOrder == "location" ? "location_desc" : "location";
            ViewData["SeasonSortParm"] = sortOrder == "season" ? "season_desc" : "season";
            ViewData["CurrentFilter"] = paginationDTO.KeyWord;

            IEnumerable<CreateOrEditHotelViewModel> hotels = _mapper.Map<IEnumerable<HotelDTO>, IEnumerable<CreateOrEditHotelViewModel>>(_adminManager.GetHotels(paginationDTO, sortOrder));

            HotelsViewModel model = new HotelsViewModel
            {
                hotels = hotels,
                PaginationDTO = paginationDTO
            };


            return View(model);
        }

        [HttpGet]
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

        [HttpGet]
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
            hotel.HotelPhotos = await _photoManager.InsertHotelPhotoAsync(model.HotelPhotos, model.Id);
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

        [HttpPost]
        public async Task<IActionResult> DeleteHotel(int Id)
        {
            await _adminManager.DeleteHotel(Id);
            return RedirectToAction("Hotels");
        }

        #endregion
        #region HotelConvs

        [HttpGet]
        public IActionResult HotelConvs(int Id, AdminPaginationDTO paginationDTO, string sortOrder)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "price" ? "price_desc" : "price";
            ViewData["CurrentFilter"] = paginationDTO.KeyWord;
            ViewData["Id"] = Id;

            IEnumerable<HotelConvsViewModel> hotelConvs = _mapper
                .Map<IEnumerable<HotelConvDTO>, IEnumerable<HotelConvsViewModel>>(_adminManager.GetHotelConvs(paginationDTO, sortOrder)
                .Where(hc => hc.HotelId == Id));


            IEnumerableHotelConvsViewModel model = new IEnumerableHotelConvsViewModel
            {
                hotelConvs = hotelConvs,
                PaginationDTO = paginationDTO
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteHotelConv(int Id, int HotelId)
        {
            await _adminManager.DeleteHotelConv(Id);
            return RedirectToAction("HotelConvs", new { Id = HotelId });
        }

        [HttpGet]
        public IActionResult CreateHotelConv(int Id)
        {
            CreateOrEditHotelConvViewModel hotelConv = new CreateOrEditHotelConvViewModel
            {
                HotelId = Id,
            };
            IEnumerable<AdditionalConvDTO> additionalConvs = _adminManager.GetAdditionalConvs();
            CreateAndEditHotelConvViewModel model = new CreateAndEditHotelConvViewModel
            {
                additionalConvs = additionalConvs,
                model = hotelConv
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHotelConv(CreateOrEditHotelConvViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            HotelConvDTO hotelConvDTO = _mapper.Map<CreateOrEditHotelConvViewModel, HotelConvDTO>(model);
            var res = await _adminManager.CreateHotelConv(hotelConvDTO);
            if (res.Succedeed)
                return RedirectToAction("HotelConvs", new { Id = model.HotelId });
            else
                ModelState.AddModelError(res.Property, res.Message);
            IEnumerable<AdditionalConvDTO> additionalConvs = _adminManager.GetAdditionalConvs();
            CreateAndEditHotelConvViewModel modelRes = new CreateAndEditHotelConvViewModel
            {
                additionalConvs = additionalConvs,
                model = model
            };
            return View(modelRes);
        }


        [HttpGet]
        public IActionResult EditHotelConv(int Id)
        {
            var hotelConv = _mapper.Map<HotelConvDTO, CreateOrEditHotelConvViewModel>(_adminManager.GetHotelConvById(Id));
            IEnumerable<AdditionalConvDTO> additionalConvs = _adminManager.GetAdditionalConvs();
            CreateAndEditHotelConvViewModel model = new CreateAndEditHotelConvViewModel
            {
                additionalConvs = additionalConvs,
                model = hotelConv
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditHotelConv(CreateOrEditHotelConvViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            HotelConvDTO hotelConvDTO = _mapper.Map<CreateOrEditHotelConvViewModel, HotelConvDTO>(model);
            var res = await _adminManager.EditHotelConv(hotelConvDTO);
            int HotelId = model.HotelId;
            if (res.Succedeed)
                return RedirectToAction("HotelConvs", new { Id = HotelId });
            else
                ModelState.AddModelError(res.Property, res.Message);
            IEnumerable<AdditionalConvDTO> additionalConvs = _adminManager.GetAdditionalConvs();
            CreateAndEditHotelConvViewModel modelRes = new CreateAndEditHotelConvViewModel
            {
                additionalConvs = additionalConvs,
                model = model
            };
            return View(modelRes);
        }

        #endregion
        #region HotelRooms
        [HttpGet]
        public IActionResult HotelRooms(int Id, AdminPaginationDTO paginationDTO, string sortOrder)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "number_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "price" ? "price_desc" : "price";
            ViewData["TypeSortParm"] = sortOrder == "type" ? "type_desc" : "type";
            ViewData["CurrentFilter"] = paginationDTO.KeyWord;
            ViewData["Id"] = Id;

            IEnumerable<HotelRoomsViewModel> hotelRooms = _mapper
                .Map<IEnumerable<HotelRoomDTO>, IEnumerable<HotelRoomsViewModel>>(_adminManager.GetHotelRooms(paginationDTO, sortOrder)
                .Where(hr => hr.HotelId == Id));

            IEnumerableHotelRoomsViewModel model = new IEnumerableHotelRoomsViewModel
            {
                hotelRooms = hotelRooms,
                PaginationDTO = paginationDTO
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteHotelRoom(int Id, int HotelId)
        {
            await _adminManager.DeleteHotelRoom(Id);
            return RedirectToAction("HotelRooms", new { Id = HotelId });
        }

        [HttpGet]
        public IActionResult CreateHotelRoom(int Id)
        {
            CreateOrEditHotelRoomViewModel model = new CreateOrEditHotelRoomViewModel
            {
                HotelId = Id
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CreateHotelRoom(CreateOrEditHotelRoomViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            HotelRoomDTO hotelRoom = _mapper
                .Map<CreateOrEditHotelRoomViewModel, HotelRoomDTO>(model);

            var res = await _adminManager.CreateHotelRoom(hotelRoom);

            if (res.Succedeed)
                return RedirectToAction("HotelRooms", new { Id = model.HotelId });
            else
                ModelState.AddModelError(res.Property, res.Message);

            return View(model);
        }

        [HttpGet]
        public IActionResult EditHotelRoom(int Id)
        {
            CreateOrEditHotelRoomViewModel room = _mapper
                .Map<HotelRoomDTO, CreateOrEditHotelRoomViewModel>(_adminManager.GetHotelRoomById(Id));
            return View(room);
        }

        [HttpPost]
        public async Task<IActionResult> EditHotelRoom(CreateOrEditHotelRoomViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            HotelRoomDTO hotelRoom = _mapper.Map<CreateOrEditHotelRoomViewModel, HotelRoomDTO>(model);
            var res = await _adminManager.EditHotelRoom(hotelRoom);
            if (res.Succedeed)
                return RedirectToAction("HotelRooms", new { Id = model.HotelId });
            else
                ModelState.AddModelError(res.Property, res.Message);
            return View(model);
        }

        #endregion
        #region HotelRoomConvs

        [HttpGet]
        public IActionResult HotelRoomConvs(int Id, AdminPaginationDTO paginationDTO, string sortOrder)
        {
            HotelRoomDTO room = _adminManager.GetHotelRoomById(Id);

            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "price" ? "price_desc" : "price";
            ViewData["CurrentFilter"] = paginationDTO.KeyWord;
            ViewData["Id"] = Id;
            ViewData["HotelId"] = room.HotelId;

            IEnumerable<HotelRoomConvsViewModel> convs = _mapper
                .Map<IEnumerable<HotelRoomConvDTO>, IEnumerable<HotelRoomConvsViewModel>>(_adminManager.GetRoomConvs(Id, paginationDTO, sortOrder));

            IEnumerableHotelRoomConvsViewModel model = new IEnumerableHotelRoomConvsViewModel
            {
                roomConvs = convs,
                PaginationDTO = paginationDTO
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteHotelRoomConv(int Id, int HotelRoomId)
        {
            await _adminManager.DeleteHotelRoomConv(Id);
            return RedirectToAction("HotelRoomConvs", new { Id = HotelRoomId });
        }

        [HttpGet]
        public IActionResult AddConvForRoom(int Id, int HotelId)
        {
            IEnumerable<HotelConvsViewModel> hotelConvs = _mapper
                .Map<IEnumerable<HotelConvDTO>, IEnumerable<HotelConvsViewModel>>(_adminManager.GetHotelConvs(null, null)
                .Where(hc => hc.HotelId == HotelId));

            IEnumerable<HotelRoomConvsViewModel> convs = _mapper
                .Map<IEnumerable<HotelRoomConvDTO>, IEnumerable<HotelRoomConvsViewModel>>(_adminManager.GetRoomConvs(Id, null, null));

            List<HotelRoomConvsViewModel> res = new List<HotelRoomConvsViewModel>();

            int i = 0;
            foreach (var hc in hotelConvs)
            {
                if (!convs.Any(c => c.ConvName == hc.Name))
                {
                    i++;
                    HotelRoomConvsViewModel roomConv = new HotelRoomConvsViewModel
                    {
                        Id = i,
                        Price = hc.Price,
                        ConvName = hc.Name,
                        HotelRoomId = Id
                    };
                    res.Add(roomConv);
                }

            }
            AddRoomConvViewModel model = new AddRoomConvViewModel
            {
                convs = res,
                HotelRoomId = Id,
                HotelId = HotelId
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddConvForRoom(AddRoomConvViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.SelectedItems != null)
            {
                foreach (var convName in model.SelectedItems)
                {
                    HotelRoomConvDTO conv = new HotelRoomConvDTO
                    {
                        ConvName = convName,
                        HotelRoomId = model.HotelRoomId
                    };
                    var res = await _adminManager.CreateRoomConv(conv);
                    if (!res.Succedeed)
                        ModelState.AddModelError(res.Property, res.Message);


                }
                if (ModelState.IsValid)
                    return RedirectToAction("HotelRoomConvs", new { Id = model.HotelRoomId });
            }
            else
                return RedirectToAction("HotelRoomConvs", new { Id = model.HotelRoomId });

            IEnumerable<HotelConvsViewModel> hotelConvs = _mapper
                .Map<IEnumerable<HotelConvDTO>, IEnumerable<HotelConvsViewModel>>(_adminManager.GetHotelConvs(new AdminPaginationDTO(), null)
                .Where(hc => hc.HotelId == model.HotelId));

            IEnumerable<HotelRoomConvsViewModel> convs = _mapper
                .Map<IEnumerable<HotelRoomConvDTO>, IEnumerable<HotelRoomConvsViewModel>>(_adminManager.GetRoomConvs(model.HotelRoomId, new AdminPaginationDTO(), null));

            List<HotelRoomConvsViewModel> viewResult = new List<HotelRoomConvsViewModel>();
            int i = 0;
            foreach (var hc in hotelConvs)
            {
                if (!convs.Any(c => c.ConvName == hc.Name))
                {
                    i++;
                    HotelRoomConvsViewModel roomConv = new HotelRoomConvsViewModel
                    {
                        Id = i,
                        Price = hc.Price,
                        ConvName = hc.Name,
                        HotelRoomId = model.HotelRoomId
                    };
                    viewResult.Add(roomConv);
                }

            }
            AddRoomConvViewModel viewModel = new AddRoomConvViewModel
            {
                convs = viewResult,
                HotelRoomId = model.HotelRoomId,
                HotelId = model.HotelId
            };
            return View(viewModel);
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
                CheckOutDate = DateTimeOffset.Now
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
            AdminOrderDTO adminOrderDTO = _adminManager.GetOrderById(model.OrderID);
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

        public IActionResult EditOrderDetails(int id, int OrderID)
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
        public async Task<IActionResult> DeleteOrderDetails(int Id, int OrderID)
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
                return View(_mapper.Map<AdditionalConvDTO, ConvsViewModel>(addConv));
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