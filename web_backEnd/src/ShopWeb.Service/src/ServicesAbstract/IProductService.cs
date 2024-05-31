using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopWeb.Core.src.Common;
using ShopWeb.Service.src.DTOs;

namespace ShopWeb.Service.src.ServicesAbstract
{
    public interface IProductService
    {
        Task<IEnumerable<ProductReadDto>> GetAllProductsAsync(ProductQueryOptions options);
        Task<ProductReadDto> GetProductByIdAsync(Guid id);
        Task<IEnumerable<ProductReadDto>> GetProdyctByCategoryAsync(Guid catId);
        Task<ProductReadDto> CreateProductAsync(ProductCreateDto product);
        Task<bool> UpdateProductByIdAsync(Guid id, ProductUpdateDto product);
        Task<bool> DeleteProductByIdAsync(Guid id);
        Task<IEnumerable<ProductReadDto>> GetMostPurchasedProductsAsync(Guid limit);
    }
}