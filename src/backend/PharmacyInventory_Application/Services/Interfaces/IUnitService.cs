using PharmacyInventory_Domain.Dtos.Requests;
using PharmacyInventory_Domain.Dtos.Responses;
using PharmacyInventory_Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyInventory_Application.Services.Interfaces
{
    public interface IUnitService
    {
        Task<StandardResponse<UnitResponseDto>> CreateUnitAsync(UnitRequestDto unitRequestDto);
        Task<StandardResponse<string>> DeleteUnit(string id);
        Task<StandardResponse<UnitResponseDto>> UpdateUnit(string id, UnitRequestDto unitRequestDto);
        Task<StandardResponse<UnitResponseDto>> GetUnitById(string id);
    }
}
