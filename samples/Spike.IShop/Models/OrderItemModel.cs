using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spike.IShop.Models
{
    public class OrderItemModel
    {
        public long OrderItemID { get; set; }

        public long OrderID { get; set; }

        public string OrderItemName { get; set; }
    }
}
