﻿using PharmacyInventory_Domain.Dtos.Requests;
using PharmacyInventory_Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyInventory_Domain.Dtos.Responses
{
    public class UnitResponseDto : AuditableBaseEntityDto
    {
        public string Id { get; set; }
        public UnitType UnitType { get; set; }
        public double ConversionFactor { get; set; }

    }
}
