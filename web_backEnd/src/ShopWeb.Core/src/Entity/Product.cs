using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWeb.Core.src.Entity
{
    public class Product
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "The Name field must be between 2 and 100 characters.")]
        [RegularExpression(@"^[A-Za-z]", ErrorMessage = "Name must start with a letter.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "The Category Id field is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Category Id must be a valid number.")]
        public int CategoryId { get; set; }
        // Navigation property for Order


        [Required(ErrorMessage = "The Price field is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "The Price field must be a non-negative number.")]
        public decimal Price { get; set; }


        // [StringLength(255, ErrorMessage = "The Image field cannot exceed 255 characters.")]
        // public string Image { get; set; } = string.Empty;

        [Required(ErrorMessage = "The Description field is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "The Description field must be between 2 and 100 characters.")]
        public string Description { get; set; } = string.Empty;

        public ICollection<ImageUrl> imageUrls { get; set; }
        public Category? Category { get; set; }

    }
}