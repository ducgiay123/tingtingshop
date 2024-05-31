using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWeb.Service.src.DTOs
{
    public class OrderCreateDto
    {
        // public Guid UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid AddressId { get; set; }
        public ICollection<OrderItemCreateDto> OrderItems { get; set; }

    }
    public class OrderReadDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public AddressReadDto Address { get; set; }
        public ICollection<OrderItemReadDto> OrderItems { get; set; }
    }
    public class OrderUpdateDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid AddressId { get; set; }
        public ICollection<OrderItemUpdateDto> OrderItems { get; set; }
    }
}