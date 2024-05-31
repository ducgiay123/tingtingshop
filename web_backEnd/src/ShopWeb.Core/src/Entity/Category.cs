using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWeb.Core.src.Entity
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "The Name must be between 2 and 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "The Description cannot exceed 500 characters.")]
        public string Description { get; set; } = string.Empty;

        [Url(ErrorMessage = "The Image URL is not valid.")]
        public string Image { get; set; } = string.Empty;
        public override string ToString()
        {
            return $"Category: Id={Id}, Name={Name}, Description={(Description ?? "N/A")}, Image={Image}";
        }
    }
}