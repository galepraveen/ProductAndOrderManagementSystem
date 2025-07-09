using System.ComponentModel.DataAnnotations;

namespace InventoryAndOrderManagementAPI.Dtos.OrderItem
{
    public class CreateOrderItemDto
    {
        [Required(ErrorMessage = "ProductId is mandatory")]
        public int ProductId { get; set; }

        [Range(1, 10000, ErrorMessage = "Quantity must be between 1 and 10000")]
        public int Quantity { get; set; }
    }
}
