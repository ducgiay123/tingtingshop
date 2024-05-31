using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ShopWeb.Core.src.Role;

namespace ShopWeb.Core.src.Entity
{
    public class User
    {
        public Guid Id { get; set; }

        // [Required(ErrorMessage = "First name is required.")]
        [MinLength(2, ErrorMessage = "First name must be at least 2 characters long.")]
        [MaxLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "First name must contain only letters and spaces.")]
        public string FirstName { get; set; } = string.Empty;

        // [Required(ErrorMessage = "Last name is required.")]
        [MinLength(2, ErrorMessage = "Last name must be at least 2 characters long.")]
        [MaxLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Last name must contain only letters and spaces.")]
        public string LastName { get; set; } = string.Empty;


        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        // [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string PasswordHash { get; set; } = string.Empty;


        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string Phone { get; set; } = string.Empty;


        [Required(ErrorMessage = "Role is required.")]
        public UserRole Role { get; set; }


        [StringLength(255, ErrorMessage = "The Image field cannot exceed 255 characters.")]
        public string? AvatarLink { get; set; } = string.Empty;


    }
}