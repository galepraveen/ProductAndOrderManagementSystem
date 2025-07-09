using InventoryAndOrderManagementAPI.Dtos.Order;
using InventoryAndOrderManagementAPI.Models;

namespace InventoryAndOrderManagementAPI.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllOrdersAsync();

        Task<Order?> GetOrderByIdAsync(int id);

        Task<Order> CreateOrderAsync(Order orderModel);

        Task<Order?> UpdateOrderStatusAsync(int id, UpdateOrderStatusDto orderDto);
    }
}
