using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopWeb.Service.src.DTOs;

namespace ShopWeb.Service.src.ServicesAbstract
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewReadDto>> GetReviewsByProductIdAsync(Guid productId);
        // Task<ReviewReadDto> GetReviewByIdAsync(Guid id);
        Task<bool> CreateReviewAsync(ReviewCreateDto reviewDto, Guid id);
        Task<bool> DeleteReviewAsync(Guid id, Guid userId);
        Task<bool> UpdateReviewAsync(ReviewUpdateDto reviewDto, Guid id);

    }
}