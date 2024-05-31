using Microsoft.EntityFrameworkCore;
using ShopWeb.API.Database;
using ShopWeb.Core.src.Entity;
using ShopWeb.Core.src.RepoAbstract;

namespace ShopWeb.API.Repo
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly WebShopDbContext _dbContext;
        public CategoryRepository(WebShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();

            return category;
        }

        public async Task<bool> DeleteCategoryByIdAsync(int id)
        {
            var category = await _dbContext.Categories.FindAsync(id);

            if (category is null)
            {
                return false;
            }

            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _dbContext.Categories.FindAsync(id);
        } 

        public async Task<Category> UpdateCategoryAsync(int id, Category category)
        {
            var existingCategory = _dbContext.Categories.FirstOrDefault(c => c.Id == id);
            if (existingCategory == null)
            {
                throw new InvalidOperationException($"Category with ID {id} not found.");
            }
            // _dbContext.Categories.Update(category);
            existingCategory.Name = category.Name;
            existingCategory.Description = category.Description;
            existingCategory.Image = category.Image;

            await _dbContext.SaveChangesAsync();

            return existingCategory;
        }

        // public Task<Category> UpdateCategoryAsync(int id, Category category)
        // {
        //     throw new NotImplementedException();
        // }
    }
}