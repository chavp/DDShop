using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Spike.Domains.Entities
{
    [Table("Party")]
    public abstract class Party
        : Entity
    {
        [Required]
        public string Name { get; set; }
    }
}
