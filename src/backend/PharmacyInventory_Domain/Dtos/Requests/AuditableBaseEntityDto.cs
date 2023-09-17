using System.ComponentModel.DataAnnotations;

namespace PharmacyInventory_Domain.Dtos.Requests
{
    public class AuditableBaseEntityDto
    {
       

        public string Name { get; set; }

       
        public string? Description { get; set; }
    }
}