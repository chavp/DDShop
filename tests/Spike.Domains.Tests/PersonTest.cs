using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spike.Domains.Tests
{
    using Entities;
    using Factories;
    using Xunit;

    public class PersonTest
    {
        [Fact]
        public void PassingTest()
        {
            var partyFactory = new PartyFactory();

            var theA = (Person)partyFactory.CreatePersonal("A");
            Assert.Equal("Hi, I'am A", theA.Hi());
        }
    }
}
