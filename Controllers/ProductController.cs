using InventoryAndOrderManagementAPI.Dtos.Product;
using InventoryAndOrderManagementAPI.Helpers;
using InventoryAndOrderManagementAPI.Interfaces;
using InventoryAndOrderManagementAPI.Mapper;
using InventoryAndOrderManagementAPI.Routes;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAndOrderManagementAPI.Controllers
{
    [Route(ApiRoutes.ProductsBase)]
    [ApiController] // It specifies that it's a WebAPI Controller, not MVC Controller that returns views
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductQueryObject queryObject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var products = await _productRepository.GetAllProductsAsync(queryObject);
            var productDtos = products.Select(product => product.ToProductDto());

            return Ok(productDtos);
        }

        [HttpGet("{productId:int}")]
        public async Task<IActionResult> GetProductById([FromRoute] int productId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var product = await _productRepository.GetProductByIdAsync(productId);

            if (product == null) return NotFound(new
            {
                message = $"Product with ID: {productId} not found"
            });

            return Ok(product.ToProductDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productModel = productDto.ConvertToProductModelFromProductDto();
            await _productRepository.CreateProductAsync(productModel);

            return CreatedAtAction(nameof(GetProductById), new {id = productModel.ProductId}, productModel.ToProductDto());
        }

        [HttpPut]
        [Route("{productId:int}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int productId, [FromBody] UpdateProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productModel = await _productRepository.UpdateProductAsync(productId, productDto);

            if (productModel == null) return NotFound( new
            {
                message = $"Product with ID: {productId} not found"
            });

            return Ok(productModel.ToProductDto());
        }

        [HttpDelete]
        [Route("{productId:int}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int productId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productModel = await _productRepository.DeleteProductAsync(productId);

            if (productModel == null) return NotFound( new
            {
                message = $"Product with ID: {productId} not found"
            });
            
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
