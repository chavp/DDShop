using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spike.Mappings.Repositories
{
    using Spike.Domains.Entities;
    using Spike.Domains.Repositories;
    using Mehdime.Entity;

    public class OrderRepository : IOrderRepository
    {
        private readonly IAmbientDbContextLocator ambientDbContextLocator;

        private DDShopContext DbContext
        {
            get
            {
                var dbContext = this.ambientDbContextLocator.Get<DDShopContext>();

                if (dbContext == null)
                    throw new InvalidOperationException("No ambient DbContext of type DDShopContext found. This means that this repository method has been called outside of the scope of a DbContextScope. A repository must only be accessed within the scope of a DbContextScope, which takes care of creating the DbContext instances that the repositories need and making them available as ambient contexts. This is what ensures that, for any given DbContext-derived type, the same instance is used throughout the duration of a business transaction. To fix this issue, use IDbContextScopeFactory in your top-level business logic service method to create a DbContextScope that wraps the entire business transaction that your service method implements. Then access this repository within that scope. Refer to the comments in the IDbContextScope.cs file for more details.");

                return dbContext;
            }
        }

        public OrderRepository(IAmbientDbContextLocator ambientDbContextLocator)
        {
            if (ambientDbContextLocator == null) throw new ArgumentNullException("ambientDbContextLocator");
            this.ambientDbContextLocator = ambientDbContextLocator;
        }

        public Order Get(long id)
        {
            return DbContext.Orders.Find(id);
        }

        public Order Add(Order newOrder)
        {
            DbContext.Orders.Add(newOrder);
            return newOrder;
        }

        public void Remove(Order order)
        {
            DbContext.Orders.Remove(order);
        }
        public Order GetByOrderNo(string orderNo)
        {
            return (from o in DbContext.Orders
                    where o.OrderNo == orderNo
                    select o).SingleOrDefault();
        }
        public IList<Order> GetByPartyName(string name)
        {
            return (from o in DbContext.Orders
                    where o.MakeBy.Name == name
                    select o).ToList();
        }
    }
}
