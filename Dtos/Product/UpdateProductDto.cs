using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InventoryAndOrderManagementAPI.Dtos.Product
{
    public class UpdateProductDto
    {
        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(50, ErrorMessage = "Product name cannot exceed more than 50 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(2000, ErrorMessage = "Product Description can have atmost 2000 characters")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required.")]
        [Range(0, 10000000, ErrorMessage = "Price must be between range [1 - 10000000]")]
        [DefaultValue(0)]
        public decimal Price { get; set; }

        [Range(0, 10000, ErrorMessage = "QuantityInStock must be between range [1 - 1000")]
        public int QuantityInStock { get; set; }

        [Required(ErrorMessage = "CategoryId is required.")]
        public int CategoryId { get; set; }
    }
}
