using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyInventory_Domain.Entities
{
    public class Drug : AuditableBaseEntity
    {
        public double Quantity { get; set; }
        [Column(TypeName = "money")]
        public decimal PricePerUnit { get; set; }

        [ForeignKey(nameof(Unit))]
        public string UnitId { get; set; }
        public Unit Unit { get; set; }

        [ForeignKey(nameof(GenericName))]
        public string GenericNameId { get; set; }
        public GenericName GenericName { get; set; }

        [ForeignKey(nameof(Brand))]
        public string BrandId { get; set; }
        public Brand Brand { get; set; }
        public string? ImageUrl { get; set; }
        [ForeignKey(nameof(Supplier))]
        public string SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public DateTime ExpireDate { get; set; }
        
      
    }
}
