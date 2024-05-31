using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopWeb.Core.src.Common;
using ShopWeb.Service.src.DTOs;
using ShopWeb.Service.src.ServicesAbstract;

namespace ShopWeb.Controller.src
{
    [Route("api/v1/reviews")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        // POST: api/Reviews
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] ReviewCreateDto reviewDto)
        {
            string? userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                throw AppException.Unauthorized("User is not logged in");
            }
            Guid guidId = Guid.Parse(userId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _reviewService.CreateReviewAsync(reviewDto, guidId);

            if (!result)
            {
                throw AppException.InternalServerError("Failed to create review");
            }

            return Ok(reviewDto); ;
        }

        // GET: api/Reviews/{id}
        // [HttpGet("{id}")]
        // public async Task<IActionResult> GetReviewById(Guid id)
        // {
        //     var review = await _reviewService.GetReviewByReviewIdAsync(id);

        //     if (review == null)
        //     {
        //         return NotFound();
        //     }

        //     return Ok(review);
        // }

        // GET: api/Reviews/Product/{productId}
        [HttpGet("product/{productId}")]
        public async Task<IActionResult> GetReviewsByProductId(Guid productId)
        {
            var reviews = await _reviewService.GetReviewsByProductIdAsync(productId);

            if (reviews == null)
            {
                return NotFound();
            }

            return Ok(reviews);
        }

        // GET: api/Reviews/User/{userId}
        // [HttpGet("User/{userId}")]
        // public async Task<IActionResult> GetReviewsByUserId(Guid userId)
        // {
        //     var reviews = await _reviewService.(userId);

        //     if (reviews == null)
        //     {
        //         return NotFound();
        //     }

        //     return Ok(reviews);
        // }

        // DELETE: api/Reviews/{id}
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(Guid id)
        {
            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid guidId = Guid.Parse(userId);
            var result = await _reviewService.DeleteReviewAsync(id, guidId);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // PUT: api/Reviews/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(Guid id, [FromBody] ReviewUpdateDto reviewDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _reviewService.UpdateReviewAsync(reviewDto, id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}