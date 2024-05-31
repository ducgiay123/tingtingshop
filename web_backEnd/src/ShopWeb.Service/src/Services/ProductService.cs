using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ShopWeb.Core.src.Common;
using ShopWeb.Core.src.Entity;
using ShopWeb.Core.src.RepoAbstract;
using ShopWeb.Service.src.DTOs;
using ShopWeb.Service.src.ServicesAbstract;

namespace ShopWeb.Service.src.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<ProductReadDto> CreateProductAsync(ProductCreateDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);

            var createdProduct = await _productRepository.CreateProductAsync(product);
            var productReadDto = _mapper.Map<ProductReadDto>(createdProduct);
            return productReadDto;
        }
        public async Task<IEnumerable<ProductReadDto>> GetAllProductsAsync(ProductQueryOptions queryOptions)
        {
            var products = await _productRepository.GetAllProductsAsync(queryOptions);

            return _mapper.Map<IEnumerable<ProductReadDto>>(products);
        }
        public async Task<ProductReadDto> GetProductByIdAsync(Guid id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            return _mapper.Map<ProductReadDto>(product);
        }
        public async Task<bool> UpdateProductByIdAsync(Guid id, ProductUpdateDto productDto)
        {
            // var existingProduct = await _productRepository.GetProductByIdAsync(id);
            var product = _mapper.Map<Product>(productDto);
            return await _productRepository.UpdateProductByIdAsync(id, product);
        }

        public async Task<bool> DeleteProductByIdAsync(Guid id)
        {
            return await _productRepository.DeleteProductByIdAsync(id);
        }

        public Task<IEnumerable<ProductReadDto>> GetMostPurchasedProductsAsync(Guid limit)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductReadDto>> GetProdyctByCategoryAsync(Guid catId)
        {
            var products = await _productRepository.GetProductByIdAsync(catId);

            return _mapper.Map<IEnumerable<ProductReadDto>>(products);
        }
    }
}