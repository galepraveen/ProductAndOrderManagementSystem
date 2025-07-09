using InventoryAndOrderManagementAPI.Data;
using InventoryAndOrderManagementAPI.Interfaces;
using InventoryAndOrderManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryAndOrderManagementAPI.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDBContext _context;

        public CategoryRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Category> CreateCategoryAsync(Category categoryModel)
        {
            await _context.Categories.AddAsync(categoryModel);
            await _context.SaveChangesAsync();

            return categoryModel;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
