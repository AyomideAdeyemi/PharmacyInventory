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
        Task<StandardResponse<(IEnumerable<DrugResponseDto>, MetaData)>> GetAllDrugs(DrugRequestInputParameter parameter);
        Task<StandardResponse<(IEnumerable<DrugResponseDto>, MetaData)>> GetDrugsByBrand(string brand, DrugRequestInputParameter parameter);
        Task<StandardResponse<(IEnumerable<DrugResponseDto>, MetaData)>> GetDrugsByGenericName(string genericName, DrugRequestInputParameter parameter);
        Task<StandardResponse<(IEnumerable<DrugResponseDto>, MetaData)>> GetDrugsBySupplier(string supplier, DrugRequestInputParameter parameter);
        Task<StandardResponse<(IEnumerable<DrugResponseDto>, MetaData)>> GetDrugsByExpiryDateRange(DateTime startDate, DateTime endDate, DrugRequestInputParameter parameter);
        Task<StandardResponse<DrugResponseDto>> GetDrugById(int id);
        Task<StandardResponse<DrugResponseDto>> UpdateDrug(int id, DrugRequestDto drugRequestDto);
        Task<StandardResponse<string>> DeleteDrug(int id);
        


    }
}
