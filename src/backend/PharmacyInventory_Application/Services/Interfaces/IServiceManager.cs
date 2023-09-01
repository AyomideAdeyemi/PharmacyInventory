namespace PharmacyInventory_Application.Services.Interfaces
{
    public interface IServiceManager
    {
        IDrugService DrugService { get; }
        ISupplierService SupplierService { get; }
        IBrandService BrandService { get; }
        IGenericNameService GenericNameService { get; }
    }
}
