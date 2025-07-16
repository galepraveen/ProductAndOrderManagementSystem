using InventoryAndOrderManagementAPI.Dtos.OrderItem;
using InventoryAndOrderManagementAPI.Interfaces;
using InventoryAndOrderManagementAPI.Mapper;
using InventoryAndOrderManagementAPI.Models;
using InventoryAndOrderManagementAPI.Repository;
using InventoryAndOrderManagementAPI.Routes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryAndOrderManagementAPI.Controllers
{
    [Route(ApiRoutes.OrderItemBase)]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly IOrderItemRepository _orderItemRepo;

        public OrderItemsController(IOrderItemRepository orderItemRepo)
        {
            _orderItemRepo = orderItemRepo;
        }

        [HttpGet]
        [Route("{orderId}/items")]
        public async Task<IActionResult> GetOrderItemsByOrderId([FromRoute] int orderId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var orderExists = await _orderItemRepo.GetOrderByIdAsync(orderId);

            if (orderExists == null) return BadRequest(new
            {
                message = $"Order with Id: {orderId} doesn't exists"
            });

            var orderItems = await _orderItemRepo.GetOrderItemsByOrderIdAsync(orderId);

            var orderItemsDto = orderItems.Select(orderItem => orderItem.ToOrderItemDto()).ToList();

            return Ok(orderItemsDto);
        }

        [HttpPost]
        [Route("{orderId}/items")]
        public async Task<IActionResult> AddOrderItems([FromRoute] int orderId, [FromBody] List<CreateOrderItemDto> orderItemDtos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (orderItemDtos == null || !orderItemDtos.Any()) return BadRequest(new { message = "Order item list cannot be empty." });

            var orderExists = await _orderItemRepo.GetOrderByIdAsync(orderId);

            if (orderExists == null) return NotFound(new { message = $"Order with Id: {orderId} doesn't exists" });

            var validOrderItems = new List<OrderItem>();

            foreach (var item in orderItemDtos)
            {
                var productExists = await _orderItemRepo.GetProductByIdAsync(item.ProductId);

                if (productExists == null) return BadRequest(new { message = $"Product with Id {item.ProductId} doesn't exists" });

                validOrderItems.Add(item.ToOrderItem(productExists.Price));
            }

            await _orderItemRepo.AddOrderItemsAsync(orderId, validOrderItems);

            var orderItems = validOrderItems.Select(orderItem => orderItem.ToOrderItemDto()).ToList();

            return Ok(orderItems);
        }

    }
}
