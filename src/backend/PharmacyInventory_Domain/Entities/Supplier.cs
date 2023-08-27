namespace PharmacyInventory_Domain.Entities
{
    public class Supplier : AuditableBaseEntity
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public ICollection<Drug> Drugs { get; set; }
    }
}
