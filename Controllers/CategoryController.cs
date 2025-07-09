using InventoryAndOrderManagementAPI.Dtos.Category;
using InventoryAndOrderManagementAPI.Interfaces;
using InventoryAndOrderManagementAPI.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAndOrderManagementAPI.Controllers
{
    [Route("/api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            var categoryDtos = categories.Select(category => category.ToCategoryDto());

            return Ok(categoryDtos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var categoryModel = categoryDto.ConvertToCategoryModelFromCategoryDto();

            await _categoryRepository.CreateCategoryAsync(categoryModel);

            return Ok(categoryModel.ToCategoryDto());
        }
    }
}
