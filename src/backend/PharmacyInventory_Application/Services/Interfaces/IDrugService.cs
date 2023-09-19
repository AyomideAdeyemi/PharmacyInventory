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
        Task<StandardResponse<(IEnumerable<DrugResponseDto>, MetaData)>> GetAllDrugs();
        Task<StandardResponse<(IEnumerable<DrugResponseDto>, MetaData)>> GetDrugsByBrand(string brandId);
        Task<StandardResponse<(IEnumerable<DrugResponseDto>, MetaData)>> GetDrugsByGenericName(string genericNameId);
        Task<StandardResponse<(IEnumerable<DrugResponseDto>, MetaData)>> GetDrugsBySupplier(string supplierId);
        Task<StandardResponse<(IEnumerable<DrugResponseDto>, MetaData)>> GetDrugsByExpiryDateRange(DateTime startDate, DateTime endDate);
        Task<StandardResponse<DrugResponseDto>> GetDrugById(string id);
        Task<StandardResponse<DrugResponseDto>> UpdateDrug(string id, DrugRequestDto drugRequestDto);
        Task<StandardResponse<string>> DeleteDrug(string id);
        Task<StandardResponse<(bool, string)>> UploadProfileImageAsync(string Id, IFormFile file);
        Task<StandardResponse<IEnumerable<DrugResponseDto>>> GetDrugsByQuantityRange(double minQuantity, double maxQuantity);





    }
}
