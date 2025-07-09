using InventoryAndOrderManagementAPI.Dtos.OrderItem;
using InventoryAndOrderManagementAPI.Models;

namespace InventoryAndOrderManagementAPI.Mapper
{
    public static class OrderItemMapper
    {
        public static OrderItemDto ToOrderItemDto(this OrderItem orderItem)
        {
            return new OrderItemDto
            {
                OrderItemId = orderItem.OrderItemId,
                ProductId = orderItem.ProductId,
                Quantity = orderItem.Quantity,
                Price = orderItem.Price
            };
        }

        public static OrderItem ToOrderItem(this CreateOrderItemDto createOderItemDto, decimal currentPrice)
        {
            return new OrderItem
            {
                ProductId = createOderItemDto.ProductId,
                Quantity = createOderItemDto.Quantity,
                Price = currentPrice
            };
        }
    }
}
