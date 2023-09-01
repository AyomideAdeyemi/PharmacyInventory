using PharmacyInventory_Infrastructure.Repository.Abstractions;

namespace PharmacyInventory_Infrastructure.UnitOfWorkManager
{
    public interface IUnitOfWork
    {
        IDrugRepository Drug { get; }
        ISupplierRepository Supplier { get; }
        IGenericNameRepository GenericName { get; }
        IBrandRepository Brand { get; }
        IUnitRepository Unit { get; }
        IUserRepository User { get; }
        Task SaveAsync();
    }
}
