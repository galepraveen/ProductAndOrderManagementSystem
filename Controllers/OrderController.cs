using InventoryAndOrderManagementAPI.Dtos.Order;
using InventoryAndOrderManagementAPI.Interfaces;
using InventoryAndOrderManagementAPI.Mapper;
using InventoryAndOrderManagementAPI.Models;
using InventoryAndOrderManagementAPI.Routes;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAndOrderManagementAPI.Controllers
{
    [Route(ApiRoutes.OrderBase)]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var orders = await _orderRepository.GetAllOrdersAsync();
            var orderDtos = orders.Select(order => order.ToOrderDto());

            return Ok(orderDtos);
        }

        [HttpGet("{orderId:int}")]
        public async Task<IActionResult> GetOrderById([FromRoute] int orderId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var orderModel = await _orderRepository.GetOrderByIdAsync(orderId);

            if (orderModel == null) return NotFound();

            return Ok(orderModel.ToOrderDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto orderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orderModel = orderDto.ConvertToOrderModelFromOrderDto();
            await _orderRepository.CreateOrderAsync(orderModel);

            return CreatedAtAction(nameof(GetOrderById), new { orderId = orderModel.OrderId }, orderModel.ToOrderDto());
        }

        [HttpPatch("{orderId:int}/status")]
        public async Task<IActionResult> UpdateOrderStatus([FromRoute] int orderId, [FromBody] UpdateOrderStatusDto orderStatusDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedOrderModel = await _orderRepository.UpdateOrderStatusAsync(orderId, orderStatusDto);

            if (updatedOrderModel == null)
            {
                return NotFound(new
                {
                    message = $"Order with ID {orderId} not found"
                });
            }

            return Ok(updatedOrderModel.ToOrderDto());
        }

    }
}
