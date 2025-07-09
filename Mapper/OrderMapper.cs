using InventoryAndOrderManagementAPI.Dtos.Order;
using InventoryAndOrderManagementAPI.Models;

namespace InventoryAndOrderManagementAPI.Mapper
{
    public static class OrderMapper
    {
        public static OrderDto ToOrderDto(this Order orderModel)
        {
            return new OrderDto
            {
                OrderId = orderModel.OrderId,
                CustomerId = orderModel.CustomerId,
                TotalAmount = orderModel.TotalAmount,
                Status = orderModel.Status
            };
        }

        public static Order ConvertToOrderModelFromOrderDto(this CreateOrderDto orderDto)
        {
            return new Order
            {
                CustomerId = orderDto.CustomerId,
                TotalAmount = orderDto.TotalAmount,
                Status = orderDto.Status
            };
        }
    }
}
