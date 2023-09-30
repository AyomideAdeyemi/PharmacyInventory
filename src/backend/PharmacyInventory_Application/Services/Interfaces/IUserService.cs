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
using PharmacyInventory_Domain.Entities;

namespace PharmacyInventory_Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<StandardResponse<string>> DeleteUser(string userId);
        Task<StandardResponse<UserResponseDto>> UpdateUser(string id, UserRequestDto userRequestDto);
        Task<StandardResponse<UserResponseDto>> GetUserById(string id);
        Task<StandardResponse<PagedList<UserResponseDto>>> GetAllUsersAsync(UserRequestInputParameter parameter);





    }
}
