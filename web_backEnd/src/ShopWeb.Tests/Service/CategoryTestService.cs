using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EcomShop.Application.src.DTO;
using Moq;
using ShopWeb.Core.src.Entity;
using ShopWeb.Core.src.RepoAbstract;
using ShopWeb.Service.src.DTOs;
using ShopWeb.Service.src.Services;
using Xunit;

namespace ShopWeb.Service.Tests
{
    public class CategoryServiceTests
    {
        private readonly CategoryService _categoryService;
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public CategoryServiceTests()
        {
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _mapperMock = new Mock<IMapper>();

            _categoryService = new CategoryService(_categoryRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetCategoryByIdAsync_ValidCategoryId_ReturnsCategory()
        {
            // Arrange
            var categoryId = 1;
            var category = new Category
            {
                // Set up category
            };
            _categoryRepositoryMock.Setup(x => x.GetCategoryByIdAsync(categoryId)).ReturnsAsync(category);
            _mapperMock.Setup(x => x.Map<CategoryReadDto>(category)).Returns(new CategoryReadDto());

            // Act
            var result = await _categoryService.GetCategoryByIdAsync(categoryId);

            // Assert
            Assert.NotNull(result);
            // Add additional assertions as needed
        }

        [Fact]
        public async Task GetAllCategoriesAsync_ReturnsAllCategories()
        {
            // Arrange
            var categories = new List<Category>
            {
                // Set up list of categories
            };
            _categoryRepositoryMock.Setup(x => x.GetAllCategoriesAsync()).ReturnsAsync(categories);
            _mapperMock.Setup(x => x.Map<IEnumerable<CategoryReadDto>>(categories)).Returns(new List<CategoryReadDto>());

            // Act
            var result = await _categoryService.GetAllCategoriesAsync();

            // Assert
            Assert.NotNull(result);
            // Add additional assertions as needed
        }

        [Fact]
        public async Task CreateCategoryAsync_ValidCategoryDto_ReturnsCreatedCategory()
        {
            // Arrange
            var categoryCreateDto = new CategoryCreateDto
            {
                // Set up category create DTO
            };
            var category = new Category
            {
                // Set up category
            };

            _mapperMock.Setup(x => x.Map<Category>(categoryCreateDto)).Returns(category);
            _categoryRepositoryMock.Setup(x => x.CreateCategoryAsync(category)).ReturnsAsync(category);
            _mapperMock.Setup(x => x.Map<CategoryReadDto>(category)).Returns(new CategoryReadDto());

            // Act
            var result = await _categoryService.CreateCategoryAsync(categoryCreateDto);

            // Assert
            Assert.NotNull(result);
            // Add additional assertions as needed
        }

        [Fact]
        public async Task UpdateCategoryByIdAsync_ValidCategoryIdAndCategoryDto_ReturnsTrue()
        {
            // Arrange
            var categoryId = 1;
            var categoryUpdateDto = new CategoryUpdateDto
            {
                // Set up category update DTO
            };

            var updatedCategory = new Category
            {
                // Set up updated category
            };

            _mapperMock.Setup(x => x.Map<Category>(categoryUpdateDto)).Returns(updatedCategory);
            _categoryRepositoryMock.Setup(x => x.UpdateCategoryAsync(categoryId, updatedCategory)).ReturnsAsync(updatedCategory);

            // Act
            var result = await _categoryService.UpdateCategoryByIdAsync(categoryId, categoryUpdateDto);

            // Assert
            Assert.True(result);
            // Add additional assertions as needed
        }


    }
}
