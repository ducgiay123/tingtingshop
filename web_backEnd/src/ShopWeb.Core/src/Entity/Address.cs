using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWeb.Core.src.Entity
{
    public class Address
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Street name is required.")]
        [MinLength(2, ErrorMessage = "Street name must be at least 2 characters long.")]
        [MaxLength(50, ErrorMessage = "Street name cannot exceed 50 characters.")]
        [RegularExpression(@"^[A-Za-z]", ErrorMessage = "StreetName must start with a letter.")]
        public string StreetName { get; set; } = string.Empty;

        [MinLength(1, ErrorMessage = "Street Number must be at least 1 character long.")]
        [MaxLength(5, ErrorMessage = "Street number cannot exceed 5 characters.")]
        [Range(1, int.MaxValue, ErrorMessage = "StreetNumber must be a valid number.")]
        public string StreetNumber { get; set; } = string.Empty;

        [MinLength(1, ErrorMessage = "Unit Number must be at least 1 character long.")]
        [MaxLength(5, ErrorMessage = "Unit number cannot exceed 5 characters.")]
        [Range(1, int.MaxValue, ErrorMessage = "UnitNumber must be a valid number.")]
        public string UnitNumber { get; set; } = string.Empty;


        [Required(ErrorMessage = "Postal code is required.")]
        [MinLength(5, ErrorMessage = "Postal Code must be at least 5 characters long.")]
        [MaxLength(10, ErrorMessage = "Postal Code cannot exceed 10 characters.")]
        [RegularExpression(@"^\d{5}(?:[-\s]\d{4})?$", ErrorMessage = "Invalid postal code format.")]
        public string PostalCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "City is required.")]
        [MinLength(2, ErrorMessage = "City name must be at least 2 characters long.")]
        [MaxLength(50, ErrorMessage = "City name cannot exceed 50 characters.")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "User ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "User Id must be a valid number.")]
        public Guid UserId { get; set; }

        // Navigation property for User
        public User? User { get; set; }
    }
}