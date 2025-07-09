using InventoryAndOrderManagementAPI.Models;

namespace InventoryAndOrderManagementAPI.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllCustomersAsync();
        Task<Customer> CreateCustomerAsync(Customer customer);
        Task<List<Order> > GetAllOrdersOfCustomerAsync(int id);

    }
}
