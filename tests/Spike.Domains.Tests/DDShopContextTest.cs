using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spike.Domains.Tests
{
    using Entities;
    using Mappings;
    using Mappings.Repositories;
    using Mehdime.Entity;
    using NLog;
    using Xunit;
    using DDShop;
    using Factories;
    using System.Data;
    using System.Data.Entity.Validation;
    public class DDShopContextTest
    {
        /// <summary>
        /// http://mehdi.me/ambient-dbcontext-in-ef6/
        /// </summary>

        private static Logger logger = LogManager.GetCurrentClassLogger();

        [Fact]
        public void Test_DDShopContext_Init()
        {
            // test DDShopContext
            using (var ctx = new DDShopContext())
            {
                
            }
        }

        [Fact]
        public void Create_Personal()
        {
            var dbContextScopeFactory = new DbContextScopeFactory();
            var ambientDbContextLocator = new AmbientDbContextLocator();

            using (var dbContextScope = dbContextScopeFactory.Create())
            {
                var partyFactory = new PartyFactory();
                var dingP = partyFactory.CreatePersonal("ding.p");

                var partyRepo = new PersonRepository(ambientDbContextLocator);
                partyRepo.Add(dingP);

                dbContextScope.SaveChanges();
            }
        }

        [Fact]
        public void Add_100_OrderItem()
        {
            var dbContextScopeFactory = new DbContextScopeFactory();
            var ambientDbContextLocator = new AmbientDbContextLocator();

            var orderFactory = new OrderFactory();

            using (var dbContextScope = dbContextScopeFactory.Create())
            {
                var personRepository = new PersonRepository(ambientDbContextLocator);
                var orderRepository = new OrderRepository(ambientDbContextLocator);

                var person = personRepository.GetByName("ding.p");
                var order = orderFactory.CreateOrder(person);
                orderRepository.Add(order);

                for (int i = 0; i < 100; i++)
                {
                    var orderItem = order.CreateItem("A-" + i);

                    order.OrderLine.Add(orderItem);
                }

                dbContextScope.SaveChanges();
            }
        }

        [Fact]
        public void Query_Order()
        {
            using (var ctx = new DDShopContext())
            using (var tran = ctx.Database.BeginTransaction())
            {
                ctx.Database.Log = logger.Info;

                var order = (from o in ctx.Orders.Include("OrderLine")
                             where o.MakeById == 2
                             select o).Single();

                var orderItems = order.OrderLine.ToArray();
                /*for (int i = 0; i < 100; i++)
                {
                    var orderItem = new OrderItem("A-" + i);
                    order.OrderLine.Add(orderItem);
                    //ctx.OrderItems.Remove(orderItems[i]);
                }*/

                //order.OrderLine.Add(orderItem);
                //order.OrderLine.Remove(order.OrderLine.First());
                //ctx.OrderItems.Remove(order.OrderLine.First());
                //ctx.SaveChanges();
                //var person = order.MakeBy;
                //tran.Commit();
            }
        }

        [Fact]
        public void Test_Mehdime_Entity()
        {
            //-- Poor-man DI - build our dependencies by hand for this demo
            var dbContextScopeFactory = new DbContextScopeFactory();
            var ambientDbContextLocator = new AmbientDbContextLocator();

            var orderRepository = new OrderRepository(ambientDbContextLocator);
            var personRepository = new PersonRepository(ambientDbContextLocator);

            var partyFactory = new PartyFactory();
            var orderFactory = new OrderFactory();

            var ddshop = new DDShop(
                dbContextScopeFactory,
                partyFactory,
                orderFactory,
                personRepository,
                orderRepository);

            using (var dbContextScope = dbContextScopeFactory.CreateWithTransaction(IsolationLevel.ReadCommitted))
            {

                var theOrder = orderRepository.Get(1);
                var member = personRepository.GetByName("ding.p");

                var orderModel = ddshop.NewOrder(member.ID);

                dbContextScope.SaveChanges();
            }

            using (var dbContextScope = dbContextScopeFactory.CreateWithTransaction(IsolationLevel.ReadCommitted))
            {
                /*var orderRepository = new OrderRepository(ambientDbContextLocator);
                var personRepository = new PersonRepository(ambientDbContextLocator);

                var ddshop = new DDShop(
                    dbContextScopeFactory,
                    personRepository, orderRepository);*/

                var theOrder = orderRepository.Get(1);
                var member = personRepository.GetByName("ding.p");

                var orderModel = ddshop.NewOrder(member.ID);

                dbContextScope.SaveChanges();
            }
        }

        [Fact]
        public void Remove_PersonRepository()
        {
            var dbContextScopeFactory = new DbContextScopeFactory();
            var ambientDbContextLocator = new AmbientDbContextLocator();

            var personRepository = new PersonRepository(ambientDbContextLocator);

            using (var dbContextScope = dbContextScopeFactory.Create())
            {
                var person = personRepository.Get(1);

                personRepository.Remove(person);

                dbContextScope.SaveChanges();
            }
        }

        [Fact]
        public void Change_Submt_Order()
        {
            var dbContextScopeFactory = new DbContextScopeFactory();
            var ambientDbContextLocator = new AmbientDbContextLocator();

            var orderRepository = new OrderRepository(ambientDbContextLocator);
            var personRepository = new PersonRepository(ambientDbContextLocator);

            using (var dbContextScope = dbContextScopeFactory.CreateWithTransaction(IsolationLevel.ReadCommitted))
            {
                var oldOrder = orderRepository.GetByOrderNo("6de3772b-6047-4d99-8bbf-bcf3d71bfc8d");
                oldOrder.Status = ValueObjects.OrderStatus.Summited;

                /*var person = personRepository.GetByName("ding.pp");
                person.Name = "ding.p";*/

                //dbContextScope.SaveChanges();
            }

            try
            {

                using (var ctx = new DDShopContext())
                {
                    var oldOrder = (from o in ctx.Orders
                                    where o.OrderNo == "6de3772b-6047-4d99-8bbf-bcf3d71bfc8d"
                                    select o).Single();

                    oldOrder.Status = ValueObjects.OrderStatus.Draft;
                    ctx.SaveChanges();
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        Console.WriteLine("Error Property Name {0} : Error Message: {1}",
                                            error.PropertyName, error.ErrorMessage);
                    }
                }
            }
        }
    }
}
