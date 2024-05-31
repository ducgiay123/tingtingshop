using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ShopWeb.Core.src.Entity;
using ShopWeb.Core.src.RepoAbstract;
using ShopWeb.Service.src.DTOs;
using ShopWeb.Service.src.ServicesAbstract;

namespace ShopWeb.Service.src.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public ReviewService(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        public async Task<bool> CreateReviewAsync(ReviewCreateDto reviewDto, Guid userId)
        {
            try
            {
                var review = _mapper.Map<Review>(reviewDto);
                review.UserId = userId;
                var result = await _reviewRepository.AddReviewAsync(review);
                return result != null;
            }
            catch
            {
                // Optionally log the exception here
                // e.g., _logger.LogError(ex, "Error creating review");
                return false;
            }
        }

        public async Task<bool> DeleteReviewAsync(Guid id, Guid userId)
        {
            try
            {
                return await _reviewRepository.DeleteReviewAsync(id);
            }
            catch
            {
                // Optionally log the exception here
                return false;
            }
        }

        public async Task<IEnumerable<ReviewReadDto>> GetReviewsByProductIdAsync(Guid productId)
        {
            try
            {
                var reviews = await _reviewRepository.GetReviewByProductIdAsync(productId);

                return _mapper.Map<IEnumerable<ReviewReadDto>>(reviews);
            }
            catch
            {
                // Optionally log the exception here
                return Enumerable.Empty<ReviewReadDto>();
            }
        }

        public async Task<IEnumerable<ReviewReadDto>> GetReviewsByUserIdAsync(Guid userId)
        {
            try
            {
                var reviews = await _reviewRepository.GetReviewByUserIdAsync(userId);
                return _mapper.Map<IEnumerable<ReviewReadDto>>(reviews);
            }
            catch
            {
                // Optionally log the exception here
                return Enumerable.Empty<ReviewReadDto>();
            }
        }

        public async Task<bool> UpdateReviewAsync(ReviewUpdateDto reviewDto, Guid id)
        {
            try
            {
                var existingReview = await _reviewRepository.GetReviewByReviewIdAsync(id);
                if (existingReview == null)
                {
                    return false;
                }

                // Update the properties of the existing review
                existingReview.Comment = reviewDto.Comment ?? existingReview.Comment;
                existingReview.Rating = reviewDto.Rating ?? existingReview.Rating;

                var updatedReview = await _reviewRepository.UpdateReviewAsync(existingReview, id);
                return updatedReview != null;
            }
            catch
            {
                // Optionally log the exception here
                return false;
            }
        }
    }
}
