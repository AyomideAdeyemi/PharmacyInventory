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
        Task<PagedList<Drug>> GetDrugsByBrand(string brand, DrugRequestInputParameter parameter);
        Task<PagedList<Drug>> GetDrugsByGenericName(string genericname, DrugRequestInputParameter parameter);
        Task<PagedList<Drug>> GetDrugsBySupplier(string supplier, DrugRequestInputParameter parameter);
        Task<PagedList<Drug>> GetDrugsByExpiryDateRange(DateTime startDate, DateTime endDate, DrugRequestInputParameter parameter);
        Task<Drug> GetdrugById(int id);
    }
}
