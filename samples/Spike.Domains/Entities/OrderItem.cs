using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spike.Domains.Entities
{
    [Table("OrderItem")]
    public class OrderItem
        : Entity
    {
        protected OrderItem() { }

        internal OrderItem(string name, long orderID)
        {
            this.Name = name;
            this.OrderId = orderID;
        }

        [Required]
        public string Name { get; set; }

        public long OrderId { get; protected set; }

        [ForeignKey("OrderId")]
        public virtual Order Order { get; protected set; }
    }
}
