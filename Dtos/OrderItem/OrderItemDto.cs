using System.ComponentModel.DataAnnotations;

namespace InventoryAndOrderManagementAPI.Dtos.OrderItem
{
    public class OrderItemDto
    {
        public int OrderItemId { get; set; }
        [Required(ErrorMessage = "ProductId is mandatory")]
        public int ProductId { get; set; }
        [Range(1, 10000, ErrorMessage = "Quantity must be between 1 and 10000")]
        public int Quantity { get; set; }
        [Range(0, 10000000, ErrorMessage = "Price must be between range [1 - 10000000]")]
        public decimal Price { get; set; }
    }
}
