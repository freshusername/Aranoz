using ApplicationCore.DTOs;
using ApplicationCore.Infrastructure;
using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IOrderManager : IDisposable
    {
        AdminOrderDTO GetOrderById(int Id);
        List<AdminOrderDTO> GetOrders();
        Task<OperationDetails> CreateOrder(AdminOrderDTO orderDTO);
        Task<OperationDetails> EditOrder(AdminOrderDTO orderDTO);
        Task DeleteOrder(int id);
        Order AdminOrderDTOtoOrder(AdminOrderDTO orderDTO);
        AdminOrderDTO OrderToAdminOrderDTO(Order order);

        AdminOrderDetailDTO GetOrderDetailById(int Id);
        List<AdminOrderDetailDTO> GetOrderDetails(int Id);
        Task<OperationDetails> CreateOrderDetails(AdminOrderDetailDTO orderDTO);
        Task<OperationDetails> EditOrderDetails(AdminOrderDetailDTO orderDTO);
        OrderDetail AdminOrderDetailDTOtoOrderDetail(AdminOrderDetailDTO orderDetailDTO);
        AdminOrderDetailDTO OrderDetailToAdminOrderDetailDTO(OrderDetail orderDetail);
        bool IsHotelExists(string HotelName);
        bool IsRoomExists(int RoomID);
        Task DeleteOrderDetails(int id);
    }
}
