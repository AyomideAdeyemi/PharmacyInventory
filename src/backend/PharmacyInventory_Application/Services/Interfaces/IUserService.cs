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
    public interface IUserService
    {
        Task<StandardResponse<UserResponseDto>> CreateUserAsync(UserRequestDto userRequestDto);
        Task<StandardResponse<string>> DeleteUser(int id);
        Task<StandardResponse<UserResponseDto>> UpdateUser(int id, UserRequestDto userRequestDto);
        Task<StandardResponse<UserResponseDto>> GetUserById(int id);
        Task<StandardResponse<(IEnumerable<UserResponseDto>, MetaData)>> GetAllUsers(UserRequestInputParameter parameter);



    }
}
