using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spike.Domains.Repositories
{
    using Spike.Domains.Entities;

    public interface IPersonRepository
    {
        Person Add(Person newPerson);

        void Remove(Person person);

        Person Get(long id);

        Person GetByName(string name);
    }
}
