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
    public interface IGenericNameService
    {
        Task<StandardResponse<GenericNameResponseDto>> CreateGenericNameAsync(GenericNameRequestDto genericNameRequestDto);
        Task<StandardResponse<string>> DeleteGenericName(string id);
        Task<StandardResponse<GenericNameResponseDto>> UpdateGenericName(string id, GenericNameRequestDto genericNameRequestDto);
        Task<StandardResponse<GenericNameResponseDto>> GetGenericNameById(string id);
        Task<StandardResponse<(IEnumerable<GenericNameResponseDto>, MetaData)>> GetAllGenericName();


    }
}
