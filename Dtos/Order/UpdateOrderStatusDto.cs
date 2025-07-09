using InventoryAndOrderManagementAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InventoryAndOrderManagementAPI.Dtos.Order
{
    public class UpdateOrderStatusDto
    {
        [Required(ErrorMessage = "Status is required")]
        public Status Status { get; set; }
    }
}
