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
        Task<PagedList<Drug>> GetAllDrugs(DrugRequestInputParameter parameter);
        Task<PagedList<Drug>> GetDrugsByGenericNameId(string genericNameId, DrugRequestInputParameter parameter);
        Task<PagedList<Drug>> GetDrugsBySupplier(string supplierId, DrugRequestInputParameter parameter);
        Task<PagedList<Drug>> GetDrugsByExpiryDateRange(DateTime startDate, DateTime endDate, DrugRequestInputParameter parameter);
        Task<Drug> GetDrugById(string id);
        Task<PagedList<Drug>> GetDrugsByQuantityRange(double minQuantity, double maxQuantity, DrugRequestInputParameter parameter);
        Task<PagedList<Drug>> GetDrugsByBrandId(string brandId, DrugRequestInputParameter parameter);



    }
}
