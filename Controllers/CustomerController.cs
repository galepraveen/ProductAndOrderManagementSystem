using InventoryAndOrderManagementAPI.Dtos.Customer;
using InventoryAndOrderManagementAPI.Interfaces;
using InventoryAndOrderManagementAPI.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAndOrderManagementAPI.Controllers
{
    [Route("/api/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepo;

        public CustomerController(ICustomerRepository customerRepo)
        {
            _customerRepo = customerRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
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
        [Route("{id}/orders")]
        public async Task<IActionResult> GetAllOrdersOfCustomer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var customerOrders = await _customerRepo.GetAllOrdersOfCustomerAsync(id);

            if (customerOrders == null) return NotFound($"No orders found for customer with ID: {id}");

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
