using InventoryAndOrderManagementAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InventoryAndOrderManagementAPI.Dtos.Order
{
    public class UpdateOrderStatusDto
    {
        [Required(ErrorMessage = "Status is required")]
        [EnumDataType(typeof(Status), ErrorMessage = "Invalid Status Value. It can be one of these ('Idle, Placed, Shipped, Delivered, Cancelled')")]
        public Status Status { get; set; }
    }
}
