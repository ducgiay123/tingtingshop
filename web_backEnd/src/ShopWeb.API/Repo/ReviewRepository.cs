using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopWeb.API.Database;
using ShopWeb.Core.src.Entity;
using ShopWeb.Core.src.RepoAbstract;

namespace ShopWeb.API.Repo
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly WebShopDbContext _dbContext;

        public ReviewRepository(WebShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Review>> GetReviewByProductIdAsync(Guid productId)
        {
            return await _dbContext.Reviews
                .Where(r => r.ProductId == productId)
                .Include(r => r.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetReviewByUserIdAsync(Guid userId)
        {
            return await _dbContext.Reviews
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }

        public async Task<Review> AddReviewAsync(Review review)
        {
            try
            {
                // Check if the customer has bought the product
                var productPurchased = await _dbContext.OrderedLineItems
                    .Join(_dbContext.Orders,
                        ol => ol.OrderId,
                        o => o.Id,
                        (ol, o) => new { OrderedLineItem = ol, Order = o })
                    .AnyAsync(joined => joined.OrderedLineItem.ProductId == review.ProductId
                                        && joined.Order.UserId == review.UserId);

                if (!productPurchased)
                {
                    throw new InvalidOperationException($"This customer {review.UserId} has not bought this product ID: {review.ProductId}");
                }

                // Add the review if the product was purchased
                _dbContext.Reviews.Add(review);
                await _dbContext.SaveChangesAsync();
                return review;
            }
            catch (Exception ex)
            {
                // Optionally log the exception here
                // e.g., _logger.LogError(ex, "Error adding review");
                throw new ApplicationException("An error occurred while adding the review.", ex);
            }
        }

        public async Task<bool> DeleteReviewAsync(Guid id)
        {
            var review = await _dbContext.Reviews.FindAsync(id);
            if (review == null)
            {
                return false;
            }

            _dbContext.Reviews.Remove(review);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Review> UpdateReviewAsync(Review review, Guid id)
        {
            var existingReview = await _dbContext.Reviews.FindAsync(id);
            if (existingReview == null)
            {
                return null;
            }

            // Update the properties of the existing review
            existingReview.Comment = review.Comment;
            existingReview.Rating = review.Rating;
            existingReview.Date = review.Date;

            _dbContext.Reviews.Update(existingReview);
            await _dbContext.SaveChangesAsync();

            return existingReview;
        }

        public async Task<Review> GetReviewByReviewIdAsync(Guid id)
        {
            return await _dbContext.Reviews.FindAsync(id);
        }


    }
}