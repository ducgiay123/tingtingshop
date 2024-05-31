using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopWeb.Core.src.Common;
using ShopWeb.Core.src.Entity;

namespace ShopWeb.Core.src.RepoAbstract
{
    public interface IProductRepository
    {
        Task<Product> CreateProductAsync(Product product);
        Task<IEnumerable<Product>> GetAllProductsAsync(ProductQueryOptions options);
        Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(Guid categoryId);
        Task<Product> GetProductByIdAsync(Guid id);
        Task<bool> UpdateProductByIdAsync(Guid id, Product? product);
        Task<bool> DeleteProductByIdAsync(Guid id);
        Task<IEnumerable<Product>> GetMostPurchasedProductsAsync(int limit);
    }
}