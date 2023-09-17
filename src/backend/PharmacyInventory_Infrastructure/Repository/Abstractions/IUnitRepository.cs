using PharmacyInventory_Domain.Entities;
using PharmacyInventory_Shared.RequestParameter.Common;
using PharmacyInventory_Shared.RequestParameter.ModelParameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyInventory_Infrastructure.Repository.Abstractions
{
    public interface IUnitRepository : IRepositoryBase<Unit>
    {
        Task<Unit> GetUnitById(string id);
        Task<PagedList<Unit>> GetAlBrands(BrandRequestInputParameter parameter);
    }
}
