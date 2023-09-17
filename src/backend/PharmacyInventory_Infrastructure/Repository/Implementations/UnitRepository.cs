using Microsoft.EntityFrameworkCore;
using PharmacyInventory_Domain.Entities;
using PharmacyInventory_Infrastructure.Persistence;
using PharmacyInventory_Infrastructure.Repository.Abstractions;
using PharmacyInventory_Shared.RequestParameter.Common;
using PharmacyInventory_Shared.RequestParameter.ModelParameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyInventory_Infrastructure.Repository.Implementations
{
    public class UnitRepository : RepositoryBase<Unit>, IUnitRepository
    {
        private readonly DbSet<Unit> _unit;
        public UnitRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
            _unit = repositoryContext.Set<Unit>();
        }

        public async Task<Unit> GetUnitById(string id)
        {
            return await _unit.FindAsync(id);
        }


        public async Task<PagedList<Unit>> GetAlBrands(BrandRequestInputParameter parameter)
        {
            var result = await _unit.Skip((parameter.PageNumber - 1) * parameter.PageSize)
                .Take(parameter.PageSize).ToListAsync();
            var count = await _unit.CountAsync();
            return new PagedList<Unit>(result, count, parameter.PageNumber, parameter.PageSize);

        }
    }
}
