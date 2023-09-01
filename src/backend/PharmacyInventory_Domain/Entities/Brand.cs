namespace PharmacyInventory_Domain.Entities
{
    public class Brand : AuditableBaseEntity
    {
      public ICollection<Drug> Drugs { get; set; }
    }
}
