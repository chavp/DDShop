using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spike.Mappings.Configurations
{
    using Domains.Entities;
    using Spike.Domains;
    using System.ComponentModel.DataAnnotations.Schema;

    public abstract class DomainConfiguration<T>
        : EntityTypeConfiguration<T> where T: Entity
    {
        public DomainConfiguration()
        {
            this.HasKey(x => x.Id);
            this.Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
