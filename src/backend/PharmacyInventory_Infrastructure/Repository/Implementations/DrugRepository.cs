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

        public async Task<Drug> GetDrugById(string id)
        {
            return await _drugs.FindAsync(id);
        }

        
        public async Task<PagedList<Drug>> GetAllDrugs(DrugRequestInputParameter parameter)
        {
            var result =  _drugs
                .OrderBy(e => e.Name);
            return await PagedList<Drug>.GetPagination(result, parameter.PageNumber, parameter.PageSize);

        }
        public async Task<PagedList<Drug>> GetDrugsByBrandId(string brandId, DrugRequestInputParameter parameter)
        {
            var result = _drugs.Where(e => e.BrandId.ToLower().Contains(brandId))
                .OrderBy(e => e.Name);
            return await PagedList<Drug>.GetPagination(result, parameter.PageNumber, parameter.PageSize);
        }
       
     
        public async Task<PagedList<Drug>> GetDrugsByGenericNameId(string genericNameId, DrugRequestInputParameter parameter)
        {

            var result = _drugs.Where(c => c.GenericNameId.ToLower().Contains(genericNameId))
                .OrderBy(c => c.Name);
            return await PagedList<Drug>.GetPagination(result, parameter.PageNumber, parameter.PageSize);
        }

        public async Task<PagedList<Drug>> GetDrugsBySupplier(string supplierId, DrugRequestInputParameter parameter)
        {
            var result = _drugs.Where(c => c.SupplierId.ToLower().Contains(supplierId))
                .OrderBy(c => c.Name);
            return await PagedList<Drug>.GetPagination(result, parameter.PageNumber, parameter.PageSize);
        }


        public async Task<PagedList<Drug>> GetDrugsByExpiryDateRange(DateTime startDate, DateTime endDate, DrugRequestInputParameter parameter)
        {
            var result = _drugs.Where(drug => drug.ExpireDate >= startDate && drug.ExpireDate <= endDate);
            return await PagedList<Drug>.GetPagination(result, parameter.PageNumber, parameter.PageSize);
        }

        public async Task<IEnumerable<Drug>> GetDrugsByQuantityRange(double minQuantity, double maxQuantity)
        {
            return await _drugs
                .Where(d => d.Quantity >= minQuantity && d.Quantity <= maxQuantity)
                .ToListAsync();
        }
       


    }

}
