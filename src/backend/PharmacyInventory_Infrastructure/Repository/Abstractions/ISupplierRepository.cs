using Microsoft.EntityFrameworkCore;
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
    public interface ISupplierRepository : IRepositoryBase<Supplier>
    {
        Task<Supplier> GetSupplierById(string id);
        Task<PagedList<Supplier>> GetAllSupplier(SupplierRequestInputParameter parameter);
    }
}
