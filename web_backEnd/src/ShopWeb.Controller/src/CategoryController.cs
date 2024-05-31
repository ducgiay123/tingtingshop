using EcomShop.Application.src.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopWeb.Core.src.Common;
using ShopWeb.Service.src.ServicesAbstract;


namespace ShopWeb.API.Controllers
{
    [ApiController]
    [Route("api/v1/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryReadDto>>> GetAllCategoriesAsync()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoryReadDto>> GetCategoryByIdAsync([FromRoute] int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                throw AppException.NotFound($"Category with ID {id} not found.");
            }
            return Ok(category);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<CategoryReadDto>> CreateCategory([FromBody] CategoryCreateDto categoryDto)
        {
            if (categoryDto == null)
            {
                throw AppException.BadRequest("Invalid request body.");
            }

            var createdCategory = await _categoryService.CreateCategoryAsync(categoryDto);
            return CreatedAtAction(nameof(GetCategoryByIdAsync), new { id = createdCategory.Id }, createdCategory);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("{id:int}")]
        public async Task<ActionResult<CategoryReadDto>> UpdateCategory([FromRoute] int id, [FromBody] CategoryUpdateDto updateDto)
        {
            if (updateDto == null)
            {
                throw AppException.BadRequest("Invalid request body.");
            }

            var updatedCategory = await _categoryService.UpdateCategoryByIdAsync(id, updateDto);
            return Ok(updatedCategory);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteCategory([FromRoute] int id)
        {
            await _categoryService.DeleteCategoryByIdAsync(id);
            return NoContent();
        }
    }
}
