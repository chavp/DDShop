using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDShop.WebAPI.Modules
{
    using Nancy;
    using Nancy.ModelBinding;
    using Spike.IShop;
    using Spike.IShop.Models;
    using System.Threading;
    public class DDShopModule
        : Nancy.NancyModule
    {
        IShopService shopService = null;
        public DDShopModule(IShopService shopService)
            : base("ddshop")
        {
            this.shopService = shopService;

            Get["/"] = _ => "Hello World!";

            // GET /memebers/{memberID}
            Get["/members/{memberID}", runAsync: true] = async (ctx, ct) =>
            {
                Console.WriteLine("GET members " + ctx.memberID);
                var result = await this.GetMemberAsync(ctx.memberID);

                if (result == null)
                {
                    return HttpStatusCode.NotFound;
                }

                return result;
            };

            // GET /members/{memberID}/orders
            Get["/members/{memberID}/orders", runAsync: true] = async (ctx, ct) =>
            {
                Console.WriteLine("GET members orders " + ctx.memberID);
                var result = await this.GetOrdersAsync(ctx.memberID);

                if (result == null)
                {
                    return HttpStatusCode.NotFound;
                }

                return result;
            };

            // POST /members
            Post["/members", runAsync: true] = async (ctx, ct) =>
            {
                Console.WriteLine("Register new member");
                var newMemberModel = this.Bind<MemberModel>();
                ServiceResultModel result = await this.RegiterAsync(newMemberModel);
                if (result.Success)
                    return Negotiate.WithModel(new { message = result.Message })
                    .WithStatusCode(HttpStatusCode.OK);
                return Negotiate.WithModel(new { message = result.Message })
                    .WithStatusCode(HttpStatusCode.InternalServerError);
            };

            // POST /orders
            Post["/members/{memberID}/orders", runAsync: true] = async (ctx, ct) =>
            {
                Console.WriteLine("New order");
                ServiceResultModel result = await this.NewOrderAsync(ctx.memberID);
                if (result.Success)
                    return Negotiate.WithModel(new { message = result.Message })
                    .WithStatusCode(HttpStatusCode.OK);
                return Negotiate.WithModel(new { message = result.Message })
                    .WithStatusCode(HttpStatusCode.InternalServerError);
            };

            // GET /orders/{orderID}
            Get["/orders/{orderID}", runAsync: true] = async (ctx, ct) =>
            {
                Console.WriteLine("GET orders " + ctx.orderID);
                var result = await this.GetOrderAsync(ctx.orderID);

                if (result == null)
                {
                    return HttpStatusCode.NotFound;
                }

                return result;
            };

            // DELETE /orders/{orderID}
            Delete["/orders/{orderID}", runAsync: true] = async (ctx, ct) =>
            {
                Console.WriteLine("DELETE orders " + ctx.orderID);
                ServiceResultModel result = await this.RemoveOrderAsync(ctx.orderID);
                if (result.Success)
                    return Negotiate.WithModel(new { message = result.Message })
                    .WithStatusCode(HttpStatusCode.OK);
                return Negotiate.WithModel(new { message = result.Message })
                    .WithStatusCode(HttpStatusCode.InternalServerError);
            };

            // GET /orders/submit
            Put["/orders/submit", runAsync: true] = async (ctx, ct) =>
            {
                var submitOrder = this.Bind<OrderModel>();

                Console.WriteLine("GET submit " + submitOrder.OrderNo);

                ServiceResultModel result = await this.SubmitOrderAsync(submitOrder);
                if (result.Success)
                    return Negotiate.WithModel(new { message = result.Message })
                    .WithStatusCode(HttpStatusCode.OK);
                return Negotiate.WithModel(new { message = result.Message })
                    .WithStatusCode(HttpStatusCode.InternalServerError);
            };

            // POST
            Post["/orders/{orderID}/items", runAsync: true] = async (ctx, ct) =>
            {
                var orderItem = this.Bind<OrderItemModel>();

                ServiceResultModel result = await this.AddOrderItemAsync(ctx.orderID, orderItem.OrderItemName);
                if (result.Success)
                    return Negotiate.WithModel(new { message = result.Message })
                    .WithStatusCode(HttpStatusCode.OK);
                return Negotiate.WithModel(new { message = result.Message })
                    .WithStatusCode(HttpStatusCode.InternalServerError);
            };
        }

        private Task<ServiceResultModel> RegiterAsync(MemberModel newMember)
        {
            return Task.Run(() => this.shopService.Regiter(newMember));
        }

        private Task<MemberModel> GetMemberAsync(long memberID)
        {
            return Task.Run(() => this.shopService.GetMember(memberID));
        }

        private Task<ServiceResultModel> NewOrderAsync(long memberID)
        {
            return Task.Run(() => this.shopService.NewOrder(memberID));
        }

        private Task<ServiceResultModel> RemoveOrderAsync(long orderId)
        {
            return Task.Run(() => this.shopService.RemoveOrder(orderId));
        }

        private Task<ServiceResultModel> SubmitOrderAsync(OrderModel order)
        {
            return Task.Run(() => this.shopService.SubmitOrder(order));
        }

        private Task<OrderModel> GetOrderAsync(long orderId)
        {
            return Task.Run(() => this.shopService.GetOrder(orderId));
        }

        private Task<ServiceResultModel> AddOrderItemAsync(long orderId, string orderItemName)
        {
            return Task.Run(() => this.shopService.AddOrderItem(orderId, orderItemName));
        }

        private Task<IList<OrderModel>> GetOrdersAsync(long memberID)
        {
            return Task.Run(() => this.shopService.GetOrders(memberID));
        }
    }
}
