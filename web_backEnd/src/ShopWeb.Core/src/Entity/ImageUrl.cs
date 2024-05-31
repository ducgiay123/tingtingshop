using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWeb.Core.src.Entity
{
    public class ImageUrl
    {
        [Required(ErrorMessage = "User ID is required.")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "User Id must be a valid number.")]
        public Guid ProductId { get; set; }

        [StringLength(255, ErrorMessage = "The Image field cannot exceed 255 characters.")]
        public string Url { get; set; } = string.Empty;
    }
}