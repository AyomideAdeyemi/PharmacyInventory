using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyInventory_Domain.Dtos.Responses
{
    public class DrugResponseDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double Quantity { get; set; }
        public decimal PricePerUnit { get; set; }
        public string ImageUrl { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
