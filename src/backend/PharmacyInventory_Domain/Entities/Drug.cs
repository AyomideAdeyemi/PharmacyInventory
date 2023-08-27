namespace PharmacyInventory_Domain.Entities
{
    public class Drug : AuditableBaseEntity
    {
        public string DrugCode { get; set; }
        public double Quantity { get; set; }
        public decimal PricePerUnit { get; set; }
        public Unit Unit { get; set; }
        public GenericName GenericName { get; set; }
        public Brand Brand { get; set; }
        public string ImageUrl { get; set; }
        public Supplier Supplier { get; set; }
        public DateOnly ExpireDate { get; set; }
      
    }
}
