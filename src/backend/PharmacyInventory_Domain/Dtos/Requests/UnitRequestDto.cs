using PharmacyInventory_Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyInventory_Domain.Dtos.Requests
{
    public class UnitRequestDto : AuditableBaseEntityDto
    {
        public UnitType UnitType { get; set; }
        public double ConversionFactor { get; set; }

    }
}
