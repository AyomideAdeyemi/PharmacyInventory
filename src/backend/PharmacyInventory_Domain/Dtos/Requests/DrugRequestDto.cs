using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PharmacyInventory_Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyInventory_Domain.Dtos.Requests
{
    public class DrugRequestDto
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public decimal PricePerUnit { get; set; }             
        public string ImageUrl { get; set; }     
        public DateTime ExpireDate { get; set; }
    }
}
