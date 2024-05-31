using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWeb.Service.src.DTOs
{
    public class ReviewReadDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; } = string.Empty;
        public decimal Rating { get; set; }

        public string userName { get; set; } = string.Empty;

    }
    public class ReviewCreateDto
    {
        // public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public Guid ProductId { get; set; }
        public string Comment { get; set; } = string.Empty;
        public decimal Rating { get; set; }
    }
    public class ReviewUpdateDto
    {
        public string? Comment { get; set; }
        public decimal? Rating { get; set; }
    }
}