using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyInventory_Domain.Entities
{
    public class Brand : AuditableBaseEntity
    {
      public ICollection<Drug> Drugs { get; set; }
    }
}
