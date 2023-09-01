using Microsoft.AspNetCore.Identity;
using PharmacyInventory_Domain.Dtos.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyInventory_Application.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> RegisterUser(UserRequestDto userRequestDto);
        Task<bool> ValidateUser(UserLoginDto userLoginDto);
        Task<string> CreateToken();
    }
}
