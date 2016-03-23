using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spike.Domains.Entities
{
    public abstract class Entity
    {
        public const string Schema = "MIE";

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; protected set; }
        
    }
}