using InventoryAndOrderManagementAPI.Dtos.Product;
using InventoryAndOrderManagementAPI.Interfaces;
using InventoryAndOrderManagementAPI.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAndOrderManagementAPI.Controllers
{
    [Route("api/products")]
    [ApiController] // It specifies that it's a WebAPI Controller, not MVC Controller that returns views
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var products = await _productRepository.GetAllProductsAsync();
            var productDtos = products.Select(product => product.ToProductDto());

            return Ok(productDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var product = await _productRepository.GetProductByIdAsync(id);

            if (product == null) return NotFound();

            return Ok(product.ToProductDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto productDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var productModel = productDto.ConvertToProductModelFromProductDto();
                await _productRepository.CreateProductAsync(productModel);

                return CreatedAtAction(nameof(GetProductById), new {id = productModel.ProductId}, productModel.ToProductDto());
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody] UpdateProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productModel = await _productRepository.UpdateProductAsync(id, productDto);

            if (productModel == null) return NotFound();

            return Ok(productModel.ToProductDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var productModel = await _productRepository.DeleteProductAsync(id);

                if (productModel == null) return NotFound();

            }
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            
            return NoContent();
        }

        [HttpGet()]
        [Route("lowstock/{threshold:int?}")]
        public async Task<IActionResult> GetProductsWithLowStock([FromRoute] int threshold = 5)
        {
            var products = await _productRepository.GetProductsWithLowStockAsync(threshold);
            var productDto = products.Select(product => product.ToProductDto());

            return Ok(productDto);
        }

    }
}
