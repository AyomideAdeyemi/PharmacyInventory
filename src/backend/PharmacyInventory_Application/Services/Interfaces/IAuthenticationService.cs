using PharmacyInventory_Domain.Dtos;
using PharmacyInventory_Domain.Dtos.Requests;

namespace PharmacyInventory_Application.Services.Interfaces
{
    public interface IAuthenticationService
    {
        void SendContactMessage(ContactUs contact);
        Task<StandardResponse<string>> RegisterUser(UserRequestDto requestDto);
        Task<bool> ValidateUser(UserLoginDto userLoginDto);
        Task<string> CreateToken();
        void SendConfirmationEmail(string email, string callback_url);
        Task<string> ChangePassword(string email, ChangePasswordRequestDto requestDto);
        Task<string> ResetPassword(string token, UserLoginDto requestDto);
        Task<StandardResponse<string>> GeneratePasswordResetToken(string email);
        void SendResetPasswordEmail(string email, string callback_url);
        Task<string> GenerateEmailActivationToken(string email);
        Task<StandardResponse<string>> ConfirmEmailAddress(string email, string token);
        Task<StandardResponse<string>> RegisterAdmin(UserRequestDto requestDto);


    }
}
