using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWeb.Service.src.DTOs
{
    public class ProductCreateDto
    {
        public string? Name { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<string> Images { get; set; }
        public string? Description { get; set; }
    }

    public class ProductReadDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<string> Images { get; set; }
        public string? Description { get; set; }

    }

    public class ProductUpdateDto
    {
        public string? Name { get; set; }
        public int? CategoryId { get; set; }
        public decimal? Price { get; set; }
        // public string? Image { get; set; }
        public string? Description { get; set; }


    }
}