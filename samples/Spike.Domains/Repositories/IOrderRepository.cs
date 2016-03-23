using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spike.Domains.Repositories
{
    using Spike.Domains.Entities;
    using System.Linq.Expressions;
    public interface IOrderRepository
    {
        Order Add(Order newOrder);

        void Remove(Order order);

        Order Get(long id);

        Order GetByOrderNo(string orderNo);

        IList<Order> GetByPartyName(string name);
    }
}
