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
    public class OrderLineItemRepository : IOrderLineItemsRepository
    {
        private readonly WebShopDbContext _dbContext;
        public OrderLineItemRepository(WebShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateNewOrderItem(OrderedLineItem orderedLineItem)
        {
            if (orderedLineItem == null)
            {
                throw new ArgumentNullException(nameof(orderedLineItem));
            }
            _dbContext.OrderedLineItems.Add(orderedLineItem);
            return await _dbContext.SaveChangesAsync() > 0;
        }
        public async Task<IEnumerable<OrderedLineItem>> GetOrderLineItemsByOrderIdAsync(Guid orderId)
        {
            return await _dbContext.OrderedLineItems
                .Where(item => item.OrderId == orderId)
                .ToListAsync();
        }
    }
}