using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDShop.WebAPI
{
    using Autofac;
    using Mehdime.Entity;
    using Nancy.Bootstrappers.Autofac;
    using Spike.Domains.Factories;
    using Spike.Domains.Repositories;
    using Spike.IShop;
    using Spike.Mappings.Repositories;

    public class Bootstrapper : AutofacNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(ILifetimeScope existingContainer)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<DbContextScopeFactory>()
                   .As<IDbContextScopeFactory>()
                   .SingleInstance();

            builder.RegisterType<AmbientDbContextLocator>()
                   .As<IAmbientDbContextLocator>()
                   .SingleInstance();

            builder.RegisterType<PartyFactory>()
                   .As<IPartyFactory>()
                   .SingleInstance();

            builder.RegisterType<OrderFactory>()
                   .As<IOrderFactory>()
                   .SingleInstance();

            builder.RegisterType<OrderRepository>()
                   .As<IOrderRepository>()
                   .SingleInstance();

            builder.RegisterType<PersonRepository>()
                   .As<IPersonRepository>()
                   .SingleInstance();

            builder.RegisterType<Spike.DDShop.DDShop>()
                   .As<IShopService>()
                   .SingleInstance();

            builder.Update(existingContainer.ComponentRegistry);
        }
    }
}
