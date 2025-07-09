using InventoryAndOrderManagementAPI.Dtos.Product;
using InventoryAndOrderManagementAPI.Models;

namespace InventoryAndOrderManagementAPI.Mapper
{
    public static class ProdcutMapper
    {
        public static ProductDto ToProductDto(this Product productModel)
        {
            return new ProductDto
            {
                ProductId = productModel.ProductId,
                Name = productModel.Name,
                Description = productModel.Description,
                Price = productModel.Price,
                QuantityInStock = productModel.QuantityInStock,
                CategoryId = productModel.CategoryId
            };
        }

        public static Product ConvertToProductModelFromProductDto(this CreateProductDto productDto)
        {
            return new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                QuantityInStock = productDto.QuantityInStock,
                CategoryId = productDto.CategoryId
            };
        }
    }
}
