using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopWeb.API.Database;
using ShopWeb.Core.src.Entity;
using ShopWeb.Core.src.RepoAbstract;

namespace ShopWeb.API.Repo
{
    public class OrderRepository : IOrderRepository
    {
        private readonly WebShopDbContext _dbContext;
        public OrderRepository(WebShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Order> CreateNewOrderAsync(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
            return order;
        }
        public async Task<bool> DeleteOrderByIdAsync(Guid id)
        {
            var order = await _dbContext.Orders.FindAsync(id);
            if (order == null)
                return false;

            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            var orders = await _dbContext.Orders
                .Include(o => o.Address) // Include Address navigation property
                .Include(o => o.OrderedLineItems)
                .ToListAsync();

            // Explicitly load the OrderedLineItems navigation property for each order
            // foreach (var order in orders)
            // {
            //     await _dbContext.Entry(order)
            //         .Collection(o => o.OrderedLineItems)
            //         .LoadAsync();
            // }

            return orders;
        }
        public async Task<Order> GetOrderByIdAsync(Guid id)
        {
            return await _dbContext.Orders
                .Include(o => o.Address) // Include the Address navigation property
                .Include(o => o.OrderedLineItems) // Include the OrderedLineItems collection
                .FirstOrDefaultAsync(o => o.Id == id); // Find the order by its Id
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(Guid userId)
        {
            var orders = await _dbContext.Orders.Where(o => o.UserId == userId).Include(o => o.Address).ToListAsync();
            foreach (var order in orders)
            {
                await _dbContext.Entry(order)
                    .Collection(o => o.OrderedLineItems)
                    .LoadAsync();
            }
            return orders;
        }

    }
}