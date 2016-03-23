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

    public class PersonConfiguration
        : DomainConfiguration<Person>
    {
        public PersonConfiguration()
        {
            this.ToTable("Person");

            this.Property(x => x.Name)
                .HasColumnName("Name")
                .IsRequired();

        }
    }
}
