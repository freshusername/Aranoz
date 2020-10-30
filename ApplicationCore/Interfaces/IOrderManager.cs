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
        List<Order> GetOrders();
        Task<OperationDetails> CreateOrder(OrderDTO orderDTO);
        Task DeleteOrder(int id);
    }
}
