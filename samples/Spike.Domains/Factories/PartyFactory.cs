using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spike.Domains.Entities;

namespace Spike.Domains.Factories
{
    public class PartyFactory
        : IPartyFactory
    {
        public Person CreatePersonal(string name)
        {
            return new Person(name);
        }
    }
}
