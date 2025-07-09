using InventoryAndOrderManagementAPI.Dtos.Order;
using InventoryAndOrderManagementAPI.Interfaces;
using InventoryAndOrderManagementAPI.Mapper;
using InventoryAndOrderManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAndOrderManagementAPI.Controllers
{
    [Route("api/orders")]
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

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOrderById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var orderModel = await _orderRepository.GetOrderByIdAsync(id);

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
            try
            {
                var orderModel = orderDto.ConvertToOrderModelFromOrderDto();
                await _orderRepository.CreateOrderAsync(orderModel);

                return CreatedAtAction(nameof(GetOrderById), new { id = orderModel.OrderId }, orderModel.ToOrderDto());
            }
            catch(Exception ex)
            {
                return BadRequest(new { messsage = ex.Message });
            }
        }

        [HttpPatch("{id:int}/status")]
        public async Task<IActionResult> UpdateOrderStatus([FromRoute] int id, [FromBody] UpdateOrderStatusDto orderStatusDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedOrderModel = await _orderRepository.UpdateOrderStatusAsync(id, orderStatusDto);

            if (updatedOrderModel == null) return NotFound($"Order with ID {id} not found");

            return Ok(updatedOrderModel.ToOrderDto());
        }

    }
}
