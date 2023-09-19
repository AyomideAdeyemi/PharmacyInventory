using Microsoft.EntityFrameworkCore;
using PharmacyInventory_Domain.Entities;
using PharmacyInventory_Infrastructure.Persistence;
using PharmacyInventory_Infrastructure.Repository.Abstractions;
using PharmacyInventory_Shared.RequestParameter.Common;
using PharmacyInventory_Shared.RequestParameter.ModelParameter;
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

        public async Task<Drug> GetdrugById(string id)
        {
            return await _drugs.FindAsync(id);
        }

        
        public async Task<PagedList<Drug>> GetAllDrugs()
        {
            var parameter = new DrugRequestInputParameter();
            var result = await _drugs.Skip((parameter.PageNumber - 1) * parameter.PageSize)
                .Take(parameter.PageSize).ToListAsync();
            var count = await _drugs.CountAsync();
            return new PagedList<Drug>(result, count, parameter.PageNumber, parameter.PageSize);

        }
   
        public async Task<PagedList<Drug>> GetDrugsByBrandId(string brandId)
        {
            var parameter = new DrugRequestInputParameter();
            var result = _drugs
                .AsEnumerable()
                .Where(e => e.BrandId.Contains(brandId, StringComparison.InvariantCultureIgnoreCase))
                .Skip((parameter.PageNumber - 1) * parameter.PageSize)
                .Take(parameter.PageSize)
                .OrderBy(e => e.Name)
                .ToList();
            var count = await _drugs.CountAsync();
            return new PagedList<Drug>(result, count, parameter.PageNumber, parameter.PageSize);
        }


        public async Task<PagedList<Drug>> GetDrugsByGenericNameId(string genericNameId)
        {
            var parameter = new DrugRequestInputParameter();
            var result =  _drugs
                .AsEnumerable()
                .Where(c => c.GenericNameId.Contains(genericNameId, StringComparison.InvariantCultureIgnoreCase))
                .Skip((parameter.PageNumber - 1) * parameter.PageSize)
                .Take(parameter.PageSize)
                .OrderBy(c => c.Name)
                .ToList();

            var count = await _drugs.CountAsync();
            return new PagedList<Drug>(result, count, parameter.PageNumber, parameter.PageSize);
        }

        public async Task<PagedList<Drug>> GetDrugsBySupplier(string supplierId)
        {
            var parameter = new DrugRequestInputParameter();
            var result =  _drugs
                .AsEnumerable()
                .Where(c => c.SupplierId.Contains(supplierId, StringComparison.InvariantCultureIgnoreCase))
                .Skip((parameter.PageNumber - 1) * parameter.PageSize)
                .Take(parameter.PageSize)
                .OrderBy(c => c.Name)
                .ToList();

            var count = await _drugs.CountAsync();
            return new PagedList<Drug>(result, count, parameter.PageNumber, parameter.PageSize);
        }


        public async Task<PagedList<Drug>> GetDrugsByExpiryDateRange(DateTime startDate, DateTime endDate)
        {
            var parameter = new DrugRequestInputParameter();
            var result = await _drugs
                .Where(drug => drug.ExpireDate >= startDate && drug.ExpireDate <= endDate)
                .Skip((parameter.PageNumber - 1) * parameter.PageSize)
                .Take(parameter.PageSize)
                .ToListAsync();

            var count = await _drugs.CountAsync();
            return new PagedList<Drug>(result, count, parameter.PageNumber, parameter.PageSize);
        }

        public async Task<IEnumerable<Drug>> GetDrugsByQuantityRange(double minQuantity, double maxQuantity)
        {
            return await _drugs
                .Where(d => d.Quantity >= minQuantity && d.Quantity <= maxQuantity)
                .ToListAsync();
        }
      

    }

}
