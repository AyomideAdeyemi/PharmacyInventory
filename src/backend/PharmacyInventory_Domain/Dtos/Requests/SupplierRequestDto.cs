using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyInventory_Domain.Dtos.Requests
{
    public class SupplierRequestDto : AuditableBaseEntityDto
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

    }
}
