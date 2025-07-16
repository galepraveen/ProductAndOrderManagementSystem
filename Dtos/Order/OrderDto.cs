using InventoryAndOrderManagementAPI.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InventoryAndOrderManagementAPI.Dtos.Order
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        [Required(ErrorMessage = "Customer Id is mandatory")]
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Range(0, 10000000, ErrorMessage = "TotalAmount Should be in between 0 and 10000000")]
        [DefaultValue(0)]
        public decimal TotalAmount { get; set; } = 0;

        [Required(ErrorMessage = "Status is required")]
        [EnumDataType(typeof(Status), ErrorMessage = "Invalid Status Value. It can be one of these ('Idle, Placed, Shipped, Delivered, Cancelled')")]
        public Status Status { get; set; }
    }
}
