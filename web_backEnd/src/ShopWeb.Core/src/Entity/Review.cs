using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWeb.Core.src.Entity
{
    public class Review
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The UserId field is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "User Id must be a valid number.")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "The Product Id field is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Product Id must be a valid number.")]
        public Guid ProductId { get; set; }

        [Required(ErrorMessage = "The Date field is required.")]
        public DateTime Date { get; set; }

        [Range(0, 5, ErrorMessage = "The Rating field must be between 0 and 5.")]
        public decimal Rating { get; set; }

        [StringLength(1000, ErrorMessage = "The ReviewText field cannot exceed 1000 characters.")]
        public string? Comment { get; set; }

        // Navigation property for User and Product
        public User? User { get; set; }
        public Product? Product { get; set; }

    }
}