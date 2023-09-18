using PharmacyInventory_Domain.Dtos;
using PharmacyInventory_Domain.Dtos.Requests;

namespace PharmacyInventory_Application.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<StandardResponse<string>> RegisterUser(UserRequestDto requestDto);
        Task<bool> ValidateUser(UserLoginDto userLoginDto);
        Task<string> CreateToken();
        void SendConfirmationEmail(string email, string callback_url);
        Task<string> RegisterAdmin(UserRequestDto userRequestDto);
        Task<string> ChangePassword(string email, ChangePasswordRequestDto requestDto);
        Task<string> ResetPassword(string token, UserLoginDto requestDto);
        Task<string> GeneratePasswordResetToken(string email);
        void SendResetPasswordEmail(string email, string callback_url);
        Task<string> GenerateEmailActivationToken(string email);
        Task<string> ConfirmEmailAddress(string email, string token);
        
    }
}
