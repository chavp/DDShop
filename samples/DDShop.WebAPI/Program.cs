using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDShop.WebAPI
{
    using Spike.DDShop;
    using Spike.IShop;

    using Mehdime.Entity;
    using Nancy.Hosting.Self;
    using Spike.Mappings.Repositories;
    using Spike.Domains.Repositories;
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new NancyHost(new Uri("http://localhost:1234")))
            {
                host.Start();
                Console.WriteLine("Web API starting...");
                Console.ReadLine();
            }
        }

    }
}