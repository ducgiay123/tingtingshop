using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopWeb.Core.src.Common;
using ShopWeb.Service.src.DTOs;
using ShopWeb.Service.src.ServicesAbstract;

namespace ShopWeb.Controller.src
{
    [ApiController]
    [Route("api/v1/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ProductReadDto>> CreateProductAsync([FromBody] ProductCreateDto productDto)
        {
            if (productDto == null)
            {
                return BadRequest("Invalid request body");
            }

            var createdProduct = await _productService.CreateProductAsync(productDto);
            if (createdProduct == null)
            {
                return StatusCode(500, "Failed to create product");
            }

            // Return 201 (Created) status code with the created product
            return StatusCode(201, createdProduct);
        }

        [HttpGet]
        public async Task<IEnumerable<ProductReadDto>> GetAllProductsAsync([FromQuery] ProductQueryOptions options)
        {
            var products = await _productService.GetAllProductsAsync(options);
            return products;
        }

        [HttpGet("{id:guid}")]
        public async Task<ProductReadDto> GetProductByIdAsync([FromRoute] Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return product;
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ProductReadDto>> UpdateProductByIdAsync([FromRoute] Guid id, [FromBody] ProductUpdateDto updateDto)
        {
            if (updateDto == null)
            {
                return BadRequest("Invalid request body");
            }

            var updatedProduct = await _productService.UpdateProductByIdAsync(id, updateDto);

            return Ok(updatedProduct);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteProductByIdAsync([FromRoute] Guid id)
        {
            var result = await _productService.DeleteProductByIdAsync(id);

            if (!result)
            {
                return NotFound($"Product with ID {id} not found");
            }

            return NoContent();
        }

    }
}