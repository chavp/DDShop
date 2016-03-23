using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spike.IShop
{
    using Spike.IShop.Models;

    public interface IShopService
    {
        ServiceResultModel Regiter(MemberModel newMember);

        MemberModel GetMember(long memberID);

        ServiceResultModel NewOrder(long memberID);

        ServiceResultModel RemoveOrder(long orderID);

        ServiceResultModel SubmitOrder(OrderModel order);

        OrderModel GetOrder(long orderID);

        IList<OrderModel> GetOrders(long memberID);

        ServiceResultModel AddOrderItem(long orderID, string orderItemName);


    }
}
