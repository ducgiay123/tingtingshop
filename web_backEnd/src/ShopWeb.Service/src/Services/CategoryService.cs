using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EcomShop.Application.src.DTO;
using ShopWeb.Core.src.Common;
using ShopWeb.Core.src.Entity;
using ShopWeb.Core.src.RepoAbstract;
using ShopWeb.Service.src.ServicesAbstract;

namespace ShopWeb.Service.src.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryReadDto> GetCategoryByIdAsync(int categoryId)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);

            var categoryDto = _mapper.Map<CategoryReadDto>(category);
            return categoryDto;
        }
        public async Task<IEnumerable<CategoryReadDto>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            // use mapper
            var categoryDTO = _mapper.Map<IEnumerable<CategoryReadDto>>(categories);
            return categoryDTO;
        }
        public async Task<CategoryReadDto> CreateCategoryAsync(CategoryCreateDto categoryDto)
        {
            // var category = categoryDto.CreateCategory(categoryDto);
            var category = _mapper.Map<Category>(categoryDto);
            var createdCategory = await _categoryRepository.CreateCategoryAsync(category);
            var categoryReadDto = _mapper.Map<CategoryReadDto>(createdCategory);
            return categoryReadDto;
        }
        public async Task<bool> UpdateCategoryByIdAsync(int id, CategoryUpdateDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            // Console.WriteLine(existingCategory.Name);
            // var existingCategoryDto = _mapper.Map<CategoryUpdateDto>(existingCategory);
            // existingCategory = categoryDto.UpdateCategory(existingCategory);

            return await _categoryRepository.UpdateCategoryAsync(id, category) != null;
        }

        public async Task<bool> DeleteCategoryByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

    }
}