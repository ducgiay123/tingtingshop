using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopWeb.API.Database;
using ShopWeb.Core.src.Common;
using ShopWeb.Core.src.Entity;
using ShopWeb.Core.src.RepoAbstract;

namespace ShopWeb.API.Repo
{
    public class ProductRepository : IProductRepository
    {
        private readonly WebShopDbContext _dbContext;

        public ProductRepository(WebShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Product> CreateProductAsync(Product product)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteProductByIdAsync(Guid id)
        {
            var product = await _dbContext.Products.FindAsync(id);
            if (product == null)
                return false;

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            return await _dbContext.Products.Include(p => p.imageUrls).FirstOrDefaultAsync(p => p.Id == id);
        }
        public Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(Guid categoryId)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<Product>> GetAllProductsAsync(ProductQueryOptions? options)
        {
            var query = _dbContext.Products.Include(p => p.imageUrls).AsQueryable();
            if (options != null)
            {
                if (!string.IsNullOrEmpty(options.Name))
                {

                    var lowercaseTitle = options.Name.ToLower(); // Convert title to lowercase
                    query = query.Where(p => p.Name.ToLower().Contains(lowercaseTitle));

                }
                if (options.Min_Price.HasValue)
                {
                    query = query.Where(p => p.Price >= options.Min_Price.Value);
                }
                if (options.Max_Price.HasValue)
                {
                    query = query.Where(p => p.Price <= options.Max_Price.Value);
                }
                if (!string.IsNullOrEmpty(options.SortBy))
                {
                    switch (options.SortBy.ToLower())
                    {
                        case "title":
                            query = options.SortOrder == "desc" ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name);
                            break;
                        case "price":
                            query = options.SortOrder == "desc" ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price);
                            break;

                        default:
                            // Default sorting by created date if sort by is not specified or invalid
                            query = options.SortOrder == "desc" ? query.OrderByDescending(p => p.Id) : query.OrderBy(p => p.Id);
                            break;
                    }
                }
                if (options.Category_Id.HasValue)
                {
                    query = query.Where(p => p.CategoryId == options.Category_Id);
                }
                else
                {
                    query = query.Skip(options.Offset).Take(options.Limit);
                }
            }
            var products = await query.ToListAsync(); ;
            return products;
        }
        public async Task<bool> UpdateProductByIdAsync(Guid id, Product product)
        {
            var existingProduct = await _dbContext.Products.FindAsync(id);
            if (existingProduct == null)
                return false;
            if (product.Name != null)
            {
                existingProduct.Name = product.Name;
            }
            if (product.Price != null)
            {
                existingProduct.Price = product.Price;

            }
            if (product.Description != null)
            {
                existingProduct.Description = product.Description;
            }
            if (product.CategoryId != null)
            {
                existingProduct.CategoryId = product.CategoryId;
            }

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public Task<IEnumerable<Product>> GetMostPurchasedProductsAsync(int limit)
        {
            throw new NotImplementedException();
        }


    }
}