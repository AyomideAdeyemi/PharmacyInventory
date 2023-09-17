namespace PharmacyInventory_Domain.Entities
{
    public class GenericName : AuditableBaseEntity
    {

        public ICollection<Drug> Drugs { get; set; }
    }
}
