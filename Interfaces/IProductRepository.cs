using InventoryAndOrderManagementAPI.Dtos.Product;
using InventoryAndOrderManagementAPI.Helpers;
using InventoryAndOrderManagementAPI.Models;

namespace InventoryAndOrderManagementAPI.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductsAsync(ProductQueryObject query);
        Task<Product?> GetProductByIdAsync(int id);
        Task<Product> CreateProductAsync(Product productModel);
        Task<Product?> UpdateProductAsync(int id, UpdateProductDto productDto);
        Task<Product?> DeleteProductAsync(int id);
        Task<List<Product>> GetProductsWithLowStockAsync(int threshold);
    }
}
