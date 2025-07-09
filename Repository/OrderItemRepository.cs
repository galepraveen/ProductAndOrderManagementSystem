using InventoryAndOrderManagementAPI.Data;
using InventoryAndOrderManagementAPI.Dtos.OrderItem;
using InventoryAndOrderManagementAPI.Interfaces;
using InventoryAndOrderManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryAndOrderManagementAPI.Repository
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly ApplicationDBContext _context;

        public OrderItemRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task AddOrderItemsAsync(int orderId, List<OrderItem> orderItems)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null) throw new Exception($"Order with Id: {orderId} doesn't exist");


            foreach (var item in orderItems)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                
                if (product == null) 
                    throw new Exception($"Product with Id: {item.ProductId} doesn't exists");

                if (item.Quantity > product.QuantityInStock) 
                        throw new Exception($"Requested Quantity ({item.Quantity}) for product Id {item.ProductId} exceeds available stock ({product.QuantityInStock})");

                product.QuantityInStock -= item.Quantity;

                item.OrderId = orderId;
            }
            
            await _context.OrderItems.AddRangeAsync(orderItems);
            await _context.SaveChangesAsync();

            order.TotalAmount = await _context.OrderItems
                                .Where(orderItem => orderItem.OrderId == orderId)
                                .SumAsync(orderItem => orderItem.Price * orderItem.Quantity);

            await _context.SaveChangesAsync();
        }

        public async Task<List<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId)
        {
            return await _context.OrderItems
                        .Include(orderItem => orderItem.Product)
                        .Where(orderItem => orderItem.OrderId == orderId).ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await _context.Products.FindAsync(productId);
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _context.Orders.FindAsync(orderId);
        }
    }
}
