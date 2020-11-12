using ApplicationCore.DTOs;
using ApplicationCore.Infrastructure;
using ApplicationCore.Interfaces;
using AutoMapper;
using Infrastructure.EF;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.Managers
{
    public class OrderManager : IOrderManager
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private IMapper _mapper;
        public OrderManager(ApplicationDbContext context, IMapper mapper, UserManager<AppUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }
        #region Order

        public AdminOrderDTO GetOrderById(int Id) 
        {
            Order orderFind = _context.Orders.Include(p => p.User).FirstOrDefault(p => p.Id == Id);
            if (orderFind != null)
            {
                return new AdminOrderDTO
                {
                    Id = orderFind.Id,
                    IsActive = orderFind.IsActive,
                    FirstName = orderFind.User.FirstName,
                    LastName = orderFind.User.LastName
                };
            }
            else
                return null;
        } 
        public List<AdminOrderDTO> GetOrders()
        {
            List<AdminOrderDTO> orderDTOs = new List<AdminOrderDTO>();
            List<Order> res = _context.Orders.Include(p => p.User).ToList();
            List<OrderDetail> details;
            foreach (var s in res)
            {
                details = _context.OrderDetails.Include(p => p.Order).Where(p => p.OrderId == s.Id).ToList();
                bool Isactive = false;
                if (details.Count() > 0)
                {
                    int dateIn, dateOut;
                    foreach (var d in details)
                    {
                        dateIn = DateTimeOffset.Now.CompareTo(d.CheckInDate);
                        dateOut = DateTimeOffset.Now.CompareTo(d.CheckOutDate);
                        if (dateIn == 1 && dateOut == -1)
                        {
                            Isactive = true;
                            break;
                        }
                    }
                }
                if (s.IsActive != Isactive)
                {
                    s.IsActive = Isactive;
                    _context.Orders.Update(s);
                    _context.SaveChanges();
                }

                orderDTOs.Add(new AdminOrderDTO
                {
                    Id = s.Id,
                    IsActive=Isactive,
                    FirstName = s.User.FirstName,
                    LastName = s.User.LastName
                });
            }
            return orderDTOs;
        }
        public Order AdminOrderDTOtoOrder(AdminOrderDTO orderDTO)
        {
            AppUser appUser = _userManager.Users
                    .FirstOrDefault(p => p.FirstName == orderDTO.FirstName && p.LastName == orderDTO.LastName);
            Order order = new Order
            {
                Id = orderDTO.Id,
                IsActive = orderDTO.IsActive,
                AppUserId = appUser?.Id,
                User = appUser
            };

            return order;
        }
        public AdminOrderDTO OrderToAdminOrderDTO(Order order)
        {

            AdminOrderDTO orderDTO = new AdminOrderDTO()
            {
                Id = order.Id,
                IsActive = order.IsActive,
                FirstName = order.User.FirstName,
                LastName = order.User.LastName
            };
            return orderDTO;
            
        }
        public async Task<OperationDetails> CreateOrder(AdminOrderDTO orderDTO)
        {
            Order order = AdminOrderDTOtoOrder(orderDTO);
            order.IsActive = false;
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return new OperationDetails(true, "Order is successfully added", "Id");
        }
        public async Task<OperationDetails> EditOrder(AdminOrderDTO orderDTO)
        {
            Order orderCheck = _context.Orders.FirstOrDefault(x => x.Id == orderDTO.Id);
            if (orderCheck==null)
            {
                return new OperationDetails(false, "Order is not exists", "ID");
            }
            AppUser appUser = _userManager.Users
                    .FirstOrDefault(p => p.FirstName == orderDTO.FirstName && p.LastName == orderDTO.LastName);
            orderCheck.IsActive = orderDTO.IsActive;
            orderCheck.AppUserId = appUser.Id;
            orderCheck.User = appUser;
            _context.Orders.Update(orderCheck);
            await _context.SaveChangesAsync();
            return new OperationDetails(true, "Order is updated", "ID");
        }
        public async Task DeleteOrder(int id)
        {
            Order order = _context.Orders.Find(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region OrderDetails
        public AdminOrderDetailDTO GetOrderDetailById(int id)
        {
            OrderDetail orderFind = _context.OrderDetails.Include(p => p.HotelRoom)
                                                .ThenInclude(p => p.Hotel)
                                           .Include(p => p.HotelRoom)
                                                .ThenInclude(p => p.Room)
                                           .FirstOrDefault(c => c.Id == id);
            if (orderFind != null)
            {
                AdminOrderDetailDTO orderDetailDTO = new AdminOrderDetailDTO
                {
                    Id = orderFind.Id,
                    CheckInDate = orderFind.CheckInDate,
                    CheckOutDate = orderFind.CheckOutDate,
                    
                    HotelName = orderFind.HotelRoom.Hotel.Name,
                    RoomId = orderFind.HotelRoom.Room.Id
                };
                return orderDetailDTO;
            }
            else
                return null;
        }
        public List<AdminOrderDetailDTO> GetOrderDetails(int id)
        {
            List<AdminOrderDetailDTO> orderDTOs = new List<AdminOrderDetailDTO>();
            var res = _context.OrderDetails.Where(c => c.OrderId == id)
                                            .Include(p => p.HotelRoom)
                                                .ThenInclude(p => p.Hotel)
                                           .Include(p => p.HotelRoom)
                                                .ThenInclude(p => p.Room)
                                           .OrderByDescending(p => p.OrderDate).ToList();
            foreach (var s in res)
            {
                orderDTOs.Add(new AdminOrderDetailDTO
                {
                    Id = s.Id,
                    CheckInDate = s.CheckInDate,
                    CheckOutDate = s.CheckOutDate,
                    HotelName = s.HotelRoom.Hotel.Name,
                    RoomId = s.HotelRoom.Room.Id,
                    OrderDate=s.OrderDate
                });
            }
            return orderDTOs;
        }
        public OrderDetail AdminOrderDetailDTOtoOrderDetail(AdminOrderDetailDTO orderDTO)
        {
            OrderDetail orderDetail = new OrderDetail
            {
                OrderDate = DateTimeOffset.Now,
                CheckInDate = orderDTO.CheckInDate,
                CheckOutDate = orderDTO.CheckOutDate,
                OrderId=orderDTO.OrderID
            };
            Hotel hotel = _context.Hotels.Include(p => p.HotelRooms)
                                            .ThenInclude(p => p.Room)
                                            .Include(p => p.HotelRooms)
                                            .ThenInclude(p => p.Hotel)
                                            .FirstOrDefault(p => p.Name == orderDTO.HotelName);
            
            
            Room room = _context.Rooms.FirstOrDefault(p => p.Id == orderDTO.RoomId);
            if (hotel == null || room == null)
            {
                orderDetail.HotelRoomId = 0;
                return orderDetail;
            }
            foreach (HotelRoom hr in hotel.HotelRooms)
            {
                if (room.Id == hr.RoomId)
                {
                    orderDetail.HotelRoomId = hr.Id;
                    return orderDetail;
                }
            }
            orderDetail.HotelRoomId = 0;
            return orderDetail;

        }
        public AdminOrderDetailDTO OrderDetailToAdminOrderDetailDTO(OrderDetail orderDetail)
        {
            AdminOrderDetailDTO detailDTO = new AdminOrderDetailDTO
            {
                CheckInDate = orderDetail.CheckInDate,
                CheckOutDate = orderDetail.CheckOutDate,
                Id = orderDetail.Id,
                HotelName = orderDetail.HotelRoom.Hotel.Name,
                RoomId = orderDetail.HotelRoom.Room.Id
            };
            return detailDTO;
        }
        public async Task<OperationDetails> CreateOrderDetails(AdminOrderDetailDTO orderDetailDTO)
        {
            OrderDetail order = AdminOrderDetailDTOtoOrderDetail(orderDetailDTO);
            if (order.HotelRoomId == 0)
                return new OperationDetails(false, "Room in the hotel is not exist", "HotelRoom");
            await _context.OrderDetails.AddAsync(order);
            await _context.SaveChangesAsync();
            return new OperationDetails(true, "Order details are added", "Id");
            
        }
        public async Task<OperationDetails> EditOrderDetails(AdminOrderDetailDTO orderDetailDTO)
        {
            OrderDetail orderDetailCheck = _context.OrderDetails.FirstOrDefault(x => x.Id == orderDetailDTO.Id);
            orderDetailCheck.CheckInDate = orderDetailDTO.CheckInDate.ToUniversalTime().AddDays(1);
            orderDetailCheck.CheckOutDate = orderDetailDTO.CheckOutDate.ToUniversalTime().AddDays(1);
            Hotel hotel = _context.Hotels.Include(p => p.HotelRooms)
                                            .ThenInclude(p => p.Room)
                                            .Include(p => p.HotelRooms)
                                            .ThenInclude(p => p.Hotel)
                                            .FirstOrDefault(p => p.Name == orderDetailDTO.HotelName);
            Room room = _context.Rooms.FirstOrDefault(p => p.Id == orderDetailDTO.RoomId);
            if (hotel == null)
            {
                orderDetailCheck.HotelRoomId = 0;
                return new OperationDetails(false, "Hotel is not found","HotelRoom");
            }
            if (hotel == null)
            {
                orderDetailCheck.HotelRoomId = 0;
                return new OperationDetails(false, "Room is not found", "HotelRoom");
            }
            foreach (HotelRoom hr in hotel.HotelRooms)
            {
                if (room.Id == hr.RoomId)
                {
                    orderDetailCheck.HotelRoomId = hr.Id;
                    break;
                }
            }
            _context.OrderDetails.Update(orderDetailCheck);
            await _context.SaveChangesAsync();
            return new OperationDetails(true, "Order details are updated", "ID");
        }
        public async Task DeleteOrderDetails(int id)
        {
            OrderDetail order = _context.OrderDetails.Find(id);
            _context.OrderDetails.Remove(order);
            await _context.SaveChangesAsync();
        }
        public bool IsHotelExists(string HotelName)
        {
            Hotel hotel = _context.Hotels.FirstOrDefault(p => p.Name == HotelName);
            if (hotel == null)
                return false;
            else
                return true;
        }
        public bool IsRoomExists(int RoomID)
        {
            Room room = _context.Rooms.FirstOrDefault(p => p.Id == RoomID);
            if (room == null)
                return false;
            else
                return true;
        }
        #endregion

        public void Dispose()
        {

        }
    }
}
