using PharmacyInventory_Domain.Dtos.Requests;
using PharmacyInventory_Domain.Dtos.Responses;
using PharmacyInventory_Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PharmacyInventory_Shared.RequestParameter.Common;
using PharmacyInventory_Shared.RequestParameter.ModelParameter;

namespace PharmacyInventory_Application.Services.Interfaces
{
    public interface IBrandService
    {
        Task<StandardResponse<BrandResponseDto>> CreateBrandAsync(BrandRequestDto brandRequestDto);
        Task<StandardResponse<string>> DeleteBrand(string id);
        Task<StandardResponse<BrandResponseDto>> UpdateBrand(string id, BrandRequestDto brandRequestDto);
        Task<StandardResponse<BrandResponseDto>> GetBrandById(string id);
        Task<StandardResponse<PagedList<BrandResponseDto>>> GetAllBrands(BrandRequestInputParameter parameter);


    }
}
