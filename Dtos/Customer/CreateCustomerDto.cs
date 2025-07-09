using System.ComponentModel.DataAnnotations;

namespace InventoryAndOrderManagementAPI.Dtos.Customer
{
    public class CreateCustomerDto
    {
        [Required(ErrorMessage = "Name is mandatory")]
        [StringLength(100, ErrorMessage = "Name cannot exceed more than 100 characters")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? Email { get; set; }

        [StringLength(200, ErrorMessage = "Address cannot exceed more than 200 characters")]
        public string? Address { get; set; }
    }
}
