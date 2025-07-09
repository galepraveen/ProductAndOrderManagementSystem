using InventoryAndOrderManagementAPI.Dtos.Customer;
using InventoryAndOrderManagementAPI.Models;

namespace InventoryAndOrderManagementAPI.Mapper
{
    public static class CustomerMapper
    {
        public static CustomerDto ToCustomerDto(this Customer customerModel)
        {
            return new CustomerDto
            {
                CustomerId = customerModel.CustomerId,
                Name = customerModel.Name,
                Email = customerModel.Email,
                Address = customerModel.Address
            };
        }

        public static Customer ToCustomer(this CreateCustomerDto customerDto)
        {
            return new Customer
            {
                Name = customerDto.Name,
                Email = customerDto.Email,
                Address = customerDto.Address
            };
        }
    }
}
