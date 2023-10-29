using Microsoft.AspNetCore.Http;
using PharmacyInventory_Domain.Dtos;
using PharmacyInventory_Domain.Dtos.Requests;
using PharmacyInventory_Domain.Dtos.Responses;
using PharmacyInventory_Shared.RequestParameter.Common;
using PharmacyInventory_Shared.RequestParameter.ModelParameters;

namespace PharmacyInventory_Application.Services.Interfaces
{
    public interface IDrugService 
    {
        Task<StandardResponse<DrugResponseDto>> CreateDrugAsync(DrugRequestDto drugRequestDto);
        Task<StandardResponse<PagedList<DrugResponseDto>>> GetAllDrugs(DrugRequestInputParameter parameter);
        Task<StandardResponse<PagedList<DrugResponseDto>>> GetDrugsByGenericName(string genericNameId, DrugRequestInputParameter parameter);
        Task<StandardResponse<PagedList<DrugResponseDto>>> GetDrugsBySupplier(string supplierId, DrugRequestInputParameter p);
        Task<StandardResponse<(IEnumerable<DrugResponseDto>, MetaData)>> GetDrugsByExpiryDateRange(DateTime startDate, DateTime endDate, DrugRequestInputParameter parameter  );
        Task<StandardResponse<DrugResponseDto>> GetDrugById(string id);
        Task<StandardResponse<DrugResponseDto>> UpdateDrug(string id, DrugRequestDto drugRequestDto);
        Task<StandardResponse<string>> DeleteDrug(string id);
        Task<StandardResponse<IEnumerable<DrugResponseDto>>> GetDrugsByQuantityRange(double minQuantity, double maxQuantity, DrugRequestInputParameter parameter);
        Task<StandardResponse<PagedList<DrugResponseDto>>> GetDrugsByBrandId(string brandId, DrugRequestInputParameter parameter);






    }
}
