using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spike.Domains.Factories
{
    using Spike.Domains.Entities;
    using Spike.Domains.Repositories;

    public class OrderFactory
        : IOrderFactory
    {
        public Order CreateOrder(Person customer)
        {
            var newOrder = new Order(customer);
            return newOrder;
        }
    }
}
