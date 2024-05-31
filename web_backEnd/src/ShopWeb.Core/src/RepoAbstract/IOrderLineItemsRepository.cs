using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopWeb.Core.src.Entity;

namespace ShopWeb.Core.src.RepoAbstract
{
    public interface IOrderLineItemsRepository
    {
        Task<bool> CreateNewOrderItem(OrderedLineItem orderedLineItem);
        Task<IEnumerable<OrderedLineItem>> GetOrderLineItemsByOrderIdAsync(Guid orderId);
    }
}