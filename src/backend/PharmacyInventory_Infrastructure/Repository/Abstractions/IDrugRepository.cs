using PharmacyInventory_Domain.Entities;
using PharmacyInventory_Shared.RequestParameter.Common;
using PharmacyInventory_Shared.RequestParameter.ModelParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyInventory_Infrastructure.Repository.Abstractions
{
    public interface IDrugRepository : IRepositoryBase<Drug>
    {
        Task<PagedList<Drug>> GetDrugsByBrandId(string brandId);
        Task<PagedList<Drug>> GetAllDrugs();
        Task<PagedList<Drug>> GetDrugsByGenericNameId(string genericnameId);
        Task<PagedList<Drug>> GetDrugsBySupplier(string supplierId);
        Task<PagedList<Drug>> GetDrugsByExpiryDateRange(DateTime startDate, DateTime endDate);
        Task<Drug> GetdrugById(string id);
        Task<IEnumerable<Drug>> GetDrugsByQuantityRange(double minQuantity, double maxQuantity);


    }
}
