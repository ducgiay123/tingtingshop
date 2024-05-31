using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopWeb.Core.src.Entity;

namespace ShopWeb.Core.src.RepoAbstract
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetReviewByProductIdAsync(Guid productId);
        Task<IEnumerable<Review>> GetReviewByUserIdAsync(Guid userId);
        Task<Review> GetReviewByReviewIdAsync(Guid id);
        Task<Review> AddReviewAsync(Review review);
        Task<bool> DeleteReviewAsync(Guid id);
        Task<Review> UpdateReviewAsync(Review review, Guid id);
    }
}