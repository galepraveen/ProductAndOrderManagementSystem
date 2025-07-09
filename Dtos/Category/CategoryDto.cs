using System.ComponentModel.DataAnnotations;

namespace InventoryAndOrderManagementAPI.Dtos.Category
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Category Name is mandatory.")]
        [StringLength(100, ErrorMessage = "Category name cannot exceed more than 100 characters")]
        public string? Name { get; set; }
    }
}
