using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWeb.Core.src.Entity
{
    public class Order
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The User Id field is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "User Id must be a valid number.")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "The Order Date field is required.")]
        [DataType(DataType.Date, ErrorMessage = "Order Date must be in a valid date format.")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "The Address Id field is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Address Id must be a valid number.")]
        public Guid AddressId { get; set; }

        public ICollection<OrderedLineItem> OrderedLineItems { get; set; } 

        // Navigation property for Address and User
        public User? User { get; set; }

        public Address? Address { get; set; }
    }
}