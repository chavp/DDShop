using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spike.Mappings
{
    using Domains.Entities;
    using Spike.Domains;

    public class DDShopContext: DbContext
    {
        public DDShopContext(): base()
        {
            //this.Configuration.LazyLoadingEnabled = true;
            //this.Configuration.ProxyCreationEnabled = true;
            
            //Database.SetInitializer<DDShopContext>(new CreateDatabaseIfNotExists<DDShopContext>());

            //Database.SetInitializer<DDShopContext>(new DropCreateDatabaseIfModelChanges<DDShopContext>());
            //Database.SetInitializer<DDShopContext>(new DropCreateDatabaseAlways<DDShopContext>());
            //Database.SetInitializer<DDShopContext>(new SchoolDBInitializer());

            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<DDShopContext, Spike.Mappings.Migrations.Configuration>());

            //this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

    }
}
