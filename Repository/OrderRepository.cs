using InventoryAndOrderManagementAPI.Data;
using InventoryAndOrderManagementAPI.Dtos.Order;
using InventoryAndOrderManagementAPI.Interfaces;
using InventoryAndOrderManagementAPI.Mapper;
using InventoryAndOrderManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryAndOrderManagementAPI.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDBContext _context;
        public OrderRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateOrderAsync(Order orderModel)
        {
            var customerExists = await _context.Customers.AnyAsync(customer => customer.CustomerId == orderModel.CustomerId);

            if (!customerExists)
            {
                throw new ArgumentException($"Order with Customer ID: {orderModel.CustomerId} doesn't exists");
            }

            await _context.Orders.AddAsync(orderModel);
            await _context.SaveChangesAsync();

            return orderModel;
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders.AsNoTracking().ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _context.Orders.FirstOrDefaultAsync(order => order.OrderId == id);
        }

        public async Task<Order?> UpdateOrderStatusAsync(int id, UpdateOrderStatusDto orderStatusDto)
        {
            var orderModel = await _context.Orders.FirstOrDefaultAsync(order => order.OrderId == id);

            if (orderModel == null) return null;

            orderModel.UpdateOrderStatus(orderStatusDto);

            await _context.SaveChangesAsync();

            return orderModel;
        }
    }
}
