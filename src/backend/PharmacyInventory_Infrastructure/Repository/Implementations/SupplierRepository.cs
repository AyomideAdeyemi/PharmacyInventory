using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using PharmacyInventory_Domain.Entities;
using PharmacyInventory_Infrastructure.Persistence;
using PharmacyInventory_Infrastructure.Repository.Abstractions;
using PharmacyInventory_Shared.RequestParameter.Common;
using PharmacyInventory_Shared.RequestParameter.ModelParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyInventory_Infrastructure.Repository.Implementations
{
    public class SupplierRepository : RepositoryBase<Supplier>, ISupplierRepository
    {
        private readonly DbSet<Supplier> _supplier;

        public SupplierRepository(ApplicationDbContext repositoryContext) : base (repositoryContext)
        {
            _supplier = repositoryContext.Set<Supplier>();
        }

        public async Task<Supplier> GetSupplierById(int id)
        {
            return await _supplier.FindAsync(id);
        }

         
        public async Task<PagedList<Supplier>> GetAllSupplier(SupplierRequestInputParameter parameter)
        {
            var result = await _supplier.Skip((parameter.PageNumber - 1) * parameter.PageSize)
                .Take(parameter.PageSize).ToListAsync();
            var count = await _supplier.CountAsync();
            return new PagedList<Supplier>(result, count, parameter.PageNumber, parameter.PageSize);

        }
    }
}
