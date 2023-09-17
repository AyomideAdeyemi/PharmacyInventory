using PharmacyInventory_Domain.Dtos.Requests;
using PharmacyInventory_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyInventory_Domain.Dtos.Responses
{
    public class BrandResponseDto : AuditableBaseEntityDto
    {
        public string Id { get; set; }
    }
}
