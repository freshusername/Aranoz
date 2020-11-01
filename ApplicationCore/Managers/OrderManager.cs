using ApplicationCore.DTOs;
using ApplicationCore.Infrastructure;
using ApplicationCore.Interfaces;
using AutoMapper;
using Infrastructure.EF;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Managers
{
    public class OrderManager : IOrderManager
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;
        private readonly DbSet<Order> _orders;
        public OrderManager(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _orders = _context.Orders;
            _mapper = mapper;
        }

        public List<Order> GetOrders()
        {
            return _orders.ToList();
        }

        public async Task<OperationDetails> CreateOrder(OrderDTO orderDTO)
        {
            Order orderCheck = _orders.FirstOrDefault(x => x.Id == orderDTO.Id);
            if (orderCheck == null)
            {
                Order order = _mapper.Map<OrderDTO, Order>(orderDTO);
                await _orders.AddAsync(order);
                await _context.SaveChangesAsync();
                return new OperationDetails(true, "Order added", "Id");
            }
            return new OperationDetails(false, "Order with the same id already exists", "Id");
        }

        public async Task DeleteOrder(int id)
        {
            Order order = _orders.Find(id);
            _orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            
        }
    }
}
