using Microsoft.AspNetCore.Identity;
using PharmacyInventory_Domain.Dtos;
using PharmacyInventory_Domain.Dtos.Requests;
using PharmacyInventory_Domain.Dtos.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyInventory_Application.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> RegisterUser(UserRequestDto userRequestDto);
        //  Task<StandardResponse<string>> RegisterUser(UserRequestDto userRequestDto);
        Task<IdentityResult> RegisterAdmin(UserRequestDto userRequestDto);
        Task<bool> ValidateUser(UserLoginDto userLoginDto);
        Task<string> CreateToken();
        // Task<(IdentityResult result, string emailConfirmationToken)> RegisterUser(UserRequestDto userRequestDto);
        // Task<string> RegiosterUser(UserRequestDto userRequestDto);
        void SendConfirmationEmail(string email, string callback_url);
    }
}
