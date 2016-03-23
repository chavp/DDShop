using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spike.Domains.Entities
{
    [Table("Person")]
    public class Person
        : Party
    {
        protected Person() { }

        internal Person(string name)
        {
            this.Name = name;
        }
        
        public string Hi()
        {
            return string.Format("Hi, I'am {0}", this.Name);
        }
    }
}
