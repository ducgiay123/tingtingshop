using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopWeb.Core.src.Entity;
using ShopWeb.Service.src.DTOs;

namespace ShopWeb.Service.src.ServicesAbstract
{
    public interface IOrderService
    {
        Task<OrderReadDto> CreateOrderAsync(Guid userId, OrderCreateDto order);
        Task<IEnumerable<OrderReadDto>> GetOrdersByUserIdAsync(Guid userId);
        Task<IEnumerable<OrderReadDto>> GetAllOrdersAsync();
        Task<OrderReadDto> GetOrderByIdAsync(Guid id);
        Task<bool> DeleteOrderByIdAsync(Guid id);

        // Task<OrderReadDto> UpdateOrderByIdAsync(Guid id, OrderUpdateDto orderDto);
    }
}