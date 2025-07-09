using InventoryAndOrderManagementAPI.Models;

namespace InventoryAndOrderManagementAPI.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategoriesAsync();
        Task<Category> CreateCategoryAsync(Category category);
    }
}
