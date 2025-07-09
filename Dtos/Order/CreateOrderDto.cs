using InventoryAndOrderManagementAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InventoryAndOrderManagementAPI.Dtos.Order
{
    public class CreateOrderDto
    {
        public int CustomerId { get; set; }
        [Range(0, 10000000, ErrorMessage = "TotalAmount Should be in between 0 and 10000000")]
        public decimal TotalAmount { get; set; }
        [Required(ErrorMessage = "Status is required")]
        public Status Status { get; set; }
    }
}
