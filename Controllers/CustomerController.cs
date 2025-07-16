using InventoryAndOrderManagementAPI.Dtos.Customer;
using InventoryAndOrderManagementAPI.Helpers;
using InventoryAndOrderManagementAPI.Interfaces;
using InventoryAndOrderManagementAPI.Mapper;
using InventoryAndOrderManagementAPI.Routes;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAndOrderManagementAPI.Controllers
{
    [Route(ApiRoutes.CustomerBase)]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepo;

        public CustomerController(ICustomerRepository customerRepo)
        {
            _customerRepo = customerRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers([FromQuery] ProductQueryObject queryObject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var customers = await _customerRepo.GetAllCustomersAsync();
            var customersDto = customers.Select(customer => customer.ToCustomerDto());

            return Ok(customersDto);
        }

        [HttpGet]
        [Route("{customerId}/orders")]
        public async Task<IActionResult> GetAllOrdersOfCustomer([FromRoute] int customerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var customerOrders = await _customerRepo.GetAllOrdersOfCustomerAsync(customerId);

            if (customerOrders == null) return NotFound(new { message = $"No orders found for customer with ID: {customerId}" });

            var ordersDto = customerOrders.Select(order => order.ToOrderDto()).ToList();

            return Ok(ordersDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var customer = customerDto.ToCustomer();
            await _customerRepo.CreateCustomerAsync(customer);

            return Ok(customer.ToCustomerDto());
        }
    }
}
