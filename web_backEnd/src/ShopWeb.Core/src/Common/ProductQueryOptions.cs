using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWeb.Core.src.Common
{
    public class ProductQueryOptions
    {
        public string Name { get; set; } = string.Empty;
        public decimal? Min_Price { get; set; }
        public decimal? Max_Price { get; set; }
        public int? Category_Id { get; set; }
        public string? SortBy { get; set; }
        public string? SortOrder { get; set; }
        public int Offset { get; set; } = 0;
        public int Limit { get; set; } = 10;
    }

}