using PharmacyInventory_Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyInventory_Domain.Entities
{
    public class Unit : AuditableBaseEntity 
    {

        public UnitType UnitType { get; set; }
        public double ConversionFactor { get; set; }
        public ICollection<Drug> Drugs { get; set; }

    }
}
