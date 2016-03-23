using System;
using System.Threading.Tasks;

namespace Spike.DDShop
{
    using Domains;
    using Domains.Entities;
    using Domains.Factories;
    using Domains.Repositories;
    using Domains.ValueObjects;
    using IShop.Models;
    using Mehdime.Entity;
    using Spike.IShop;
    using System.Data;
    using System.Collections.Generic;

    public class DDShop
        : IShopService
    {
        private readonly IDbContextScopeFactory dbContextScopeFactory;

        private readonly IOrderRepository orderRepository;
        private readonly IPersonRepository personRepository;

        private readonly IPartyFactory partyFactory;
        private readonly IOrderFactory orderFactory;

        public DDShop(IDbContextScopeFactory dbContextScopeFactory,
            IPartyFactory partyFactory,
            IOrderFactory orderFactory,
            IPersonRepository personRepository,
            IOrderRepository orderRepository)
        {
            if (dbContextScopeFactory == null) throw new ArgumentNullException("dbContextScopeFactory");

            if (partyFactory == null) throw new ArgumentNullException("partyFactory");
            if (orderFactory == null) throw new ArgumentNullException("orderFactory");

            if (personRepository == null) throw new ArgumentNullException("personRepository");
            if (orderRepository == null) throw new ArgumentNullException("orderRepository");

            this.dbContextScopeFactory = dbContextScopeFactory;

            this.partyFactory = partyFactory;
            this.orderFactory = orderFactory;

            this.personRepository = personRepository;
            this.orderRepository = orderRepository;
        }

        public ServiceResultModel Regiter(MemberModel newMember)
        {
            using (var dbContextScope = this.dbContextScopeFactory.Create())
            {
                var newPerson = this.partyFactory.CreatePersonal(newMember.Username);
                var person = this.personRepository.Add(newPerson);

                dbContextScope.SaveChanges();
                return new ServiceResultModel
                {
                    Success = true,
                    Message = "Add new member completed"
                };
            }
        }

        public MemberModel GetMember(long memberID)
        {
            using (var dbContextScope = this.dbContextScopeFactory.CreateReadOnly())
            {
                var person = this.personRepository.Get(memberID);
                if (person == null) return null;

                return new MemberModel
                {
                    Username = person.Name
                };
            }
        }

        public OrderModel GetOrder(long orderId)
        {
            using (var dbContextScope = this.dbContextScopeFactory.CreateReadOnly())
            {
                var order = this.orderRepository.Get(orderId);
                var orderItems = order.OrderLine;

                return new OrderModel
                {
                    OrderNo = order.OrderNo,
                    Quantity = orderItems.Count,
                };
            }
        }

        public ServiceResultModel NewOrder(long memberID)
        {
            using (var dbContextScope = this.dbContextScopeFactory.Create())
            {
                //-- Build domain model
                var makeBy = personRepository.Get(memberID);
                if(makeBy == null)
                {
                    return new ServiceResultModel
                    {
                        Success = false,
                        Message = string.Format("Not found member no " + memberID)
                    };
                }

                var newOrder = this.orderFactory.CreateOrder(makeBy);

                //-- Persist
                this.orderRepository.Add(newOrder);

                dbContextScope.SaveChanges();
            }

            return new ServiceResultModel
            {
                Success = true,
                Message = string.Format("New order completed")
            };
        }

        public ServiceResultModel RemoveOrder(long orderID)
        {
            using (var dbContextScope = this.dbContextScopeFactory.CreateWithTransaction(IsolationLevel.ReadCommitted))
            {
                var targetOrder = this.orderRepository.Get(orderID);
                this.orderRepository.Remove(targetOrder);
                dbContextScope.SaveChanges();
            }

            return new ServiceResultModel
            {
                Success = true,
                Message = string.Format("Remove order completed")
            };
        }

        public ServiceResultModel SubmitOrder(OrderModel order)
        {
            using (var dbContextScope = this.dbContextScopeFactory.CreateWithTransaction(IsolationLevel.ReadCommitted))
            {
                var targetOrder = this.orderRepository.GetByOrderNo(order.OrderNo);
                if (targetOrder == null)
                    return new ServiceResultModel
                    {
                        Success = false,
                        Message = string.Format("Not found order No {0}", order.OrderNo)
                    };

                targetOrder.Status = OrderStatus.Summited;

                dbContextScope.SaveChanges();
            }

            return new ServiceResultModel
            {
                Success = true,
                Message = string.Format("Subbmited order No {0} completed", order.OrderNo)
            };
        }

        public ServiceResultModel AddOrderItem(long orderID, string orderItemName)
        {
            using (var dbContextScope = this.dbContextScopeFactory.CreateWithTransaction(IsolationLevel.ReadCommitted))
            {
                var targetOrder = this.orderRepository.Get(orderID);
                var newItem = targetOrder.CreateItem(orderItemName);

                targetOrder.OrderLine.Add(newItem);

                dbContextScope.SaveChanges();
            }

            return new ServiceResultModel
            {
                Success = true,
                Message = string.Format("Add new order item completed")
            };
        }

        public IList<OrderModel> GetOrders(long memberID)
        {
            using (var dbContextScope = this.dbContextScopeFactory.CreateReadOnly())
            {
                var member = this.personRepository.Get(memberID);
                if (member == null) return null;

                var orderList = this.orderRepository.GetByPartyName(member.Name);
                var results = new List<OrderModel>();
                foreach (var item in orderList)
                {
                    results.Add(new OrderModel
                    {
                        OrderID = item.ID,
                        MemberID = memberID,
                        OrderNo = item.OrderNo,
                        Quantity = item.OrderLine.Count,
                        OrderStatus = item.Status.ToString()
                    });
                }
                return results;
            }
            throw new NotImplementedException();
        }
    }
}
