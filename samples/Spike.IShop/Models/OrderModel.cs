using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spike.IShop.Models
{
    public class OrderModel
    {
        public long OrderID { get; set; }
        public long MemberID { get; set; }
        public string OrderNo { get; set; }
        public long Quantity { get; set; }
        public decimal TotalCost { get; set; }
        public string InvoiceNo { get; set; }
        public string OrderStatus { get; set; }
    }
}
