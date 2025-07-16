using InventoryAndOrderManagementAPI.Data;
using InventoryAndOrderManagementAPI.Interfaces;
using InventoryAndOrderManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryAndOrderManagementAPI.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDBContext _context;

        public CustomerRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            return customer;
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers.AsNoTracking().ToListAsync();
        }

        public async Task<List<Order>> GetAllOrdersOfCustomerAsync(int customerId)
        {
            var customer = await _context.Customers.AsNoTracking().FirstOrDefaultAsync(c => c.CustomerId == customerId);

            if (customer == null)
            {
                throw new ArgumentException($"Customer Id: {customerId} does not exists");
            }

            var customerOrders = await _context.Orders.Where(order => order.CustomerId == customerId).AsNoTracking().ToListAsync();

            return customerOrders;
        }
    }
}
