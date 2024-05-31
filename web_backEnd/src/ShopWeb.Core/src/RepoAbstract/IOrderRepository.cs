using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopWeb.Core.src.Entity;

namespace ShopWeb.Core.src.RepoAbstract
{
    public interface IOrderRepository
    {
        Task<Order> CreateNewOrderAsync(Order order);
        Task<bool> DeleteOrderByIdAsync(Guid id);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(Guid id);
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(Guid userId);
    }
}