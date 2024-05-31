using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWeb.Core.src.Entity
{
    public class OrderedLineItem
    {
        public Guid Id { get; set; }

        [ForeignKey("Order")]
        [Required(ErrorMessage = "The Order Id field is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Order Id must be a valid number.")]
        public Guid OrderId { get; set; }


        [Required(ErrorMessage = "The Product Id field is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Product Id must be a valid number.")]
        public Guid ProductId { get; set; }

        [Required(ErrorMessage = "The Price field is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "The Price field must be a non-negative number.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "The Quantity field is required.")]
        [Range(1, 50, ErrorMessage = "The Quantity field must be a value between 1 and 50.")]
        public int Quantity { get; set; }


        // Navigation property for Order and Product
        public Order? Order { get; set; }
        public Product? Product { get; set; }

    }
}