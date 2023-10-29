using Microsoft.EntityFrameworkCore;
using PharmacyInventory_Domain.Entities;
using PharmacyInventory_Infrastructure.Persistence;
using PharmacyInventory_Infrastructure.Repository.Abstractions;
using PharmacyInventory_Shared.RequestParameter.Common;
using PharmacyInventory_Shared.RequestParameter.ModelParameters;

namespace PharmacyInventory_Infrastructure.Repository.Implementations
{
    public class SupplierRepository : RepositoryBase<Supplier>, ISupplierRepository
    {
        private readonly DbSet<Supplier> _supplier;

        public SupplierRepository(ApplicationDbContext repositoryContext) : base (repositoryContext)
        {
            _supplier = repositoryContext.Set<Supplier>();
        }

        public async Task<Supplier> GetSupplierById(string id)
        {
            return await _supplier.FindAsync(id);
        }

         
        public async Task<PagedList<Supplier>> GetAllSupplier(SupplierRequestInputParameter parameter)
        {
            var result =  _supplier.OrderBy(x => x.Name);
            return await PagedList<Supplier>.GetPagination(result, parameter.PageNumber, parameter.PageSize);

        }
    }
}
