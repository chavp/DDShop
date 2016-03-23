using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spike.Mappings.Configurations
{
    using Domains.Entities;
    using Spike.Domains;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;

    public class OrderConfiguration
         : DomainConfiguration<Order>
    {
        public OrderConfiguration()
        {
            this.ToTable("Order");

            this.Property(x => x.OrderNo)
                .HasColumnName("OrderNo")
                .IsRequired();

            this.HasRequired(t => t.MakeBy)
                .WithMany()
                .HasForeignKey(t => t.MakeById);

            this.HasMany<OrderItem>(o => o.OrderLine)
                .WithRequired(o => o.Order)
                .HasForeignKey(s => s.OrderId)
                .WillCascadeOnDelete(true);
        }
    }
}
