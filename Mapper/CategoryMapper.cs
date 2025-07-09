using InventoryAndOrderManagementAPI.Dtos.Category;
using InventoryAndOrderManagementAPI.Models;

namespace InventoryAndOrderManagementAPI.Mapper
{
    public static class CategoryMapper
    {
        public static CategoryDto ToCategoryDto(this Category categoryModel)
        {
            return new CategoryDto
            {
                CategoryId = categoryModel.CategoryId,
                Name = categoryModel.Name
            };
        }

        public static Category ConvertToCategoryModelFromCategoryDto(this CreateCategoryDto createCategoryDto)
        {
            return new Category
            {
                Name = createCategoryDto.Name
            };
        }

    }
}
