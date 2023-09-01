using PharmacyInventory_Domain.Dtos.Requests;
using PharmacyInventory_Domain.Dtos.Responses;
using PharmacyInventory_Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PharmacyInventory_Shared.RequestParameter.Common;
using PharmacyInventory_Shared.RequestParameter.ModelParameters;

namespace PharmacyInventory_Application.Services.Interfaces
{
    public interface ISupplierService
    {
        Task<StandardResponse<SupplierResponseDto>> CreateSupplierAsync(SupplierRequestDto supplierRequestDto);
        Task<StandardResponse<string>> DeleteSupplier(int id);
        Task<StandardResponse<SupplierResponseDto>> UpdateSupplier(int id, SupplierRequestDto supplierRequestDto);
        Task<StandardResponse<SupplierResponseDto>> GetSupplierById(int id);
        Task<StandardResponse<(IEnumerable<SupplierResponseDto>, MetaData)>> GetAllSuppliers(SupplierRequestInputParameter parameter);


    }
}
