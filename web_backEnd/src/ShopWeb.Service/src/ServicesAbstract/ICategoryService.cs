using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcomShop.Application.src.DTO;

namespace ShopWeb.Service.src.ServicesAbstract
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryReadDto>> GetAllCategoriesAsync();
        Task<CategoryReadDto> GetCategoryByIdAsync(int categoryId);
        Task<CategoryReadDto> CreateCategoryAsync(CategoryCreateDto category);
        Task<bool> UpdateCategoryByIdAsync(int id, CategoryUpdateDto category);
        Task<bool> DeleteCategoryByIdAsync(int id);
    }
}