using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spike.Domains.Factories
{
    using Spike.Domains.Entities;

    public interface IPartyFactory
    {
        Person CreatePersonal(string name);
    }
}
