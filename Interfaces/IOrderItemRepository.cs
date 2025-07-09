using InventoryAndOrderManagementAPI.Models;

namespace InventoryAndOrderManagementAPI.Interfaces
{
    public interface IOrderItemRepository
    {
        Task<List<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId);
        Task AddOrderItemsAsync(int orderId, List<OrderItem> items);

        Task<Order> GetOrderByIdAsync(int id);
        Task<Product> GetProductByIdAsync(int productId);
    }
}
