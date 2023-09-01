using Microsoft.EntityFrameworkCore;
using PharmacyInventory_Domain.Entities;
using PharmacyInventory_Infrastructure.Persistence;
using PharmacyInventory_Infrastructure.Repository.Abstractions;
using PharmacyInventory_Shared.RequestParameter.Common;
using PharmacyInventory_Shared.RequestParameter.ModelParameters;

namespace PharmacyInventory_Infrastructure.Repository.Implementations
{
    public class DrugRepository : RepositoryBase<Drug>, IDrugRepository
    {
        private readonly DbSet<Drug> _drugs;
        public DrugRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
            _drugs = repositoryContext.Set<Drug>();
        }

        public async Task<Drug> GetdrugById(int id)
        {
            return await _drugs.FindAsync(id);
        }


        public async Task<PagedList<Drug>> GetAllDrugs(DrugRequestInputParameter parameter)
        {
            var result = await _drugs.Skip((parameter.PageNumber - 1) * parameter.PageSize)
                .Take(parameter.PageSize).ToListAsync();
            var count = await _drugs.CountAsync();
            return new PagedList<Drug>(result, count, parameter.PageNumber, parameter.PageSize);

        }


        public async Task<PagedList<Drug>> GetDrugsByBrand(string brand, DrugRequestInputParameter parameter)
        {
            var result = await _drugs
                .Where(c => c.Brand.Name.Contains(brand, StringComparison.InvariantCultureIgnoreCase))
                .Skip((parameter.PageNumber - 1) * parameter.PageSize)
                .Take(parameter.PageSize)
                .ToListAsync();

            var count = await _drugs.CountAsync();
            return new PagedList<Drug>(result, count, parameter.PageNumber, parameter.PageSize);
        }

        public async Task<PagedList<Drug>> GetDrugsByGenericName(string genericname, DrugRequestInputParameter parameter)
        {
            var result = await _drugs
                .Where(c => c.GenericName.Name.Contains(genericname, StringComparison.InvariantCultureIgnoreCase))
                .Skip((parameter.PageNumber - 1) * parameter.PageSize)
                .Take(parameter.PageSize)
                .ToListAsync();

            var count = await _drugs.CountAsync();
            return new PagedList<Drug>(result, count, parameter.PageNumber, parameter.PageSize);
        }

        public async Task<PagedList<Drug>> GetDrugsBySupplier(string supplier, DrugRequestInputParameter parameter)
        {
            var result = await _drugs
                .Where(c => c.Supplier.Name.Contains(supplier, StringComparison.InvariantCultureIgnoreCase))
                .Skip((parameter.PageNumber - 1) * parameter.PageSize)
                .Take(parameter.PageSize)
                .ToListAsync();

            var count = await _drugs.CountAsync();
            return new PagedList<Drug>(result, count, parameter.PageNumber, parameter.PageSize);
        }


        public async Task<PagedList<Drug>> GetDrugsByExpiryDateRange(DateTime startDate, DateTime endDate, DrugRequestInputParameter parameter)
        {
            var result = await _drugs
                .Where(drug => drug.ExpireDate >= startDate && drug.ExpireDate <= endDate)
                .Skip((parameter.PageNumber - 1) * parameter.PageSize)
                .Take(parameter.PageSize)
                .ToListAsync();

            var count = await _drugs.CountAsync();
            return new PagedList<Drug>(result, count, parameter.PageNumber, parameter.PageSize);
        }


    }

}
