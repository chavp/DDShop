using Spike.Domains.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spike.Domains.Entities
{
    [Table("Order")]
    public class Order
        : Entity
    {
        protected Order() { }
        internal Order(Party makeBy)
        {
            Status = OrderStatus.Draft;
            MakeBy = makeBy;
            OrderLine = new List<OrderItem>();
            OrderNo = Guid.NewGuid().ToString();
        }

        public string OrderNo { get; protected set; }

        public long MakeById { get; set; }

        public virtual OrderStatus Status { get; set; }

        //[Required]
        public virtual Party MakeBy { get; protected set; }

        public virtual ICollection<OrderItem> OrderLine { get; private set; }

        public OrderItem CreateItem(string orederItemName)
        {
            return new OrderItem(orederItemName, this.ID);
        }
    }
}
