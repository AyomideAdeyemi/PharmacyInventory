using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PharmacyInventory_Application.Services.Interfaces;
using PharmacyInventory_Domain.Dtos;
using PharmacyInventory_Domain.Dtos.Requests;
using PharmacyInventory_Domain.Dtos.Responses;
using PharmacyInventory_Domain.Entities;
using PharmacyInventory_Infrastructure.UnitOfWorkManager;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;


namespace PharmacyInventory_Application.Services.Implementations
{
    public sealed class AuthenticationService : IAuthenticationService
    {
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
       
        private readonly IUnitOfWork _unitOfWork;

        private User? _user;

        public AuthenticationService(ILogger<AuthenticationService> logger, IMapper mapper, UserManager<User> userManager, IConfiguration configuration, IEmailService emailService)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _emailService = emailService;
            


        }

        public void SendContactMessage(ContactUs contact)
        {
            // string toAddress = "ayomidechinonso@gmail.com"; // Replace with your contact email address
            string toAddress = contact.Email;
            string subject = "BookShare";
            string body = $"Hello {contact.Name}<br>Thank you for reaching out to us, your message <br>({ contact.Message}) <br>has been received, as you hold on, kindly go through our About us page we promise to reply you as soon as possible ";
            try
            {
                _emailService.SendEmail(toAddress, subject, body);
                
            }
            catch (Exception ex)
            {
                // Handle any errors (log, throw, etc.)
                throw new Exception("An error occurred while sending the message.", ex);
            }
        }
        public async Task<StandardResponse<string>> RegisterUser(UserRequestDto requestDto)
        {
            var userEmail = await _userManager.FindByEmailAsync(requestDto.Email);
            if (userEmail != null)
            {
                return StandardResponse<string>.Failed($"User with this {requestDto.Email} already exist. Kindly choose another one to proceed");
            }
            User user = _mapper.Map<User>(requestDto);
            user.UserName = requestDto.Email;
            IdentityResult result = await _userManager.CreateAsync(user, requestDto.Password);
            if (result.Succeeded)
            
                {
                    await _userManager.AddToRoleAsync(user, "User");
                }
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                return StandardResponse<string>.Success("success", token);
            
        }

        
        public async Task<StandardResponse<string>> RegisterAdmin(UserRequestDto requestDto)
        {
            var userEmail = await _userManager.FindByEmailAsync(requestDto.Email);
            if (userEmail != null)
            {
                return StandardResponse<string>.Failed($"User with this {requestDto.Email} already exist. Kindly choose another one to proceed");
            }
            User user = _mapper.Map<User>(requestDto);
            user.UserName = requestDto.Email;
            IdentityResult result = await _userManager.CreateAsync(user, requestDto.Password);
            if (result.Succeeded)

            {
                await _userManager.AddToRoleAsync(user, "Admin");
            }
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            return StandardResponse<string>.Success("success", token);

        }

        public async Task<bool> ValidateUser(UserLoginDto userLoginDto)
        {
            _user = await _userManager.FindByEmailAsync(userLoginDto.Email);
            var result = (_user != null && await _userManager.CheckPasswordAsync(_user,
           userLoginDto.Password));
            if (!result)
                _logger.LogWarning($"{nameof(ValidateUser)}: Authentication failed. Wrong user name or password.");
            return result;
        }
        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET"));
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
        {
   new Claim(ClaimTypes.Name, _user.UserName)
 };
            var roles = await _userManager.GetRolesAsync(_user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials,
        List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var tokenOptions = new JwtSecurityToken
            (
            issuer: jwtSettings["validIssuer"],
            audience: jwtSettings["validAudience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
            signingCredentials: signingCredentials
            );
            return tokenOptions;
        }
        public async Task<string> GenerateEmailActivationToken(string email)
        {
            User user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return "User not found.";
            }

            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<string> ResetPassword(string token, UserLoginDto requestDto)
        {
            string trimedToken = token.Replace(" ", "+");
            User user = await _userManager.FindByEmailAsync(requestDto.Email);

            if (user == null)
            {
                return "User not found";
            }

            IdentityResult result = await _userManager.ResetPasswordAsync(user, trimedToken, requestDto.Password);

            if (result.Succeeded)
            {
                return "Password reset successful";
            }
            else
            {
                string errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += error.Description.TrimEnd('.') + ", ";
                }

                return "Password reset failed: " + errors.TrimEnd(',', ' ');
            }
        }


        public async Task<StandardResponse<string>> ConfirmEmailAddress(string email, string token)
        {
            string trimmedToken = token.Replace(" ", "+");

            User user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return StandardResponse<string>.Failed( "User not found.", 500);
            }

            if (user.EmailConfirmed)
            {
                return StandardResponse<string>.Success("Email is already confirmed.",email, 201);
            }

            IdentityResult result = await _userManager.ConfirmEmailAsync(user, trimmedToken);

            if (!result.Succeeded)
            {
                return StandardResponse<string>.Failed("Error confirming email.", 500);
            }

            return StandardResponse<string>.Success("Email confirmed successfully.", email, 201);
        }


        public void SendConfirmationEmail(string email, string callback_url)
        {
            // string logoUrl = "https://res.cloudinary.com/djbkvjfxi/image/upload/v1694601350/uf4xfoda2c4z0exly8nx.png";
            string title = "PharmTech Confirm Your Email";
            //  string body = $"<html><body><br/><br/>Please click to confirm your email address for DropMate Delivery. When you confirm your email you get full access to DropMate services for free.<p/> <a href={callback_url}>Verify Your Email</a> <p/><br/>DropMate is a game-changing delivery platform designed to simplify your life. Say goodbye to the hassles of traditional delivery services and experience a whole new level of convenience. Whether you need groceries, packages, or your favorite takeout, DropMate connects you with a network of reliable couriers who are ready to pick up and drop off your items with lightning speed. With real-time tracking, secure payments, and a seamless user interface, DropMate ensures that your deliveries are not only efficient but also stress-free. It's time to embrace a smarter way to send and receive goods – it's time for DropMate.<p/><br/><br/>With Love from the DropMate Team<p/>Thank you for choosing DropMate.<p/><img src={logoUrl}></body></html>";
            string body = $"<html><body><br/><br/>Please click to confirm access to your account" +
                $" Welcome to PharmTech office space.<p/> <a href={callback_url}>Verify Your Email</a> <p/><br/>PharmTech.<p/><br/><br/>The PharmTech Team can't wait to meet you<p/>Thank you.</body></html>";

            _emailService.SendEmail(email, title, body);
        }

        public async Task<StandardResponse<(string, UserResponseDto)>> ValidateAndCreateToken(UserLoginDto requestDto)
        {
            User user = await _userManager.FindByEmailAsync(requestDto.Email);
            bool result = await _userManager.CheckPasswordAsync(user, requestDto.Password);
            if (!result)
            {
                return StandardResponse<(string, UserResponseDto)>.Failed("An error occurred while getting all brand.", 500);

            }
            if (!(await _userManager.IsEmailConfirmedAsync(user)))
                return StandardResponse<(string, UserResponseDto)>.Failed("Email not yet confirm. Check your inbox", 500);
            string token = await CreateToken(user);
            UserResponseDto userDto = _mapper.Map<UserResponseDto>(user);
            return StandardResponse<(string, UserResponseDto)>.Success("Successful", (token, userDto));
        }
        private async Task<string> CreateToken(User user)
        {
            SigningCredentials signingCredentials = GetServerSigningCredentials();
            List<Claim> claims = await GetClaims(user);
            JwtSecurityToken tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
        private SigningCredentials GetServerSigningCredentials()
        {
            string envSecret = Environment.GetEnvironmentVariable("SECRET");
            byte[] key = Encoding.UTF8.GetBytes(envSecret);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        private async Task<List<Claim>> GetClaims(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                 new Claim(ClaimTypes.Name,user.UserName),
                 new Claim(ClaimTypes.NameIdentifier, user.Id)
            };
            IList<string> roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        public async Task <StandardResponse<string>> GeneratePasswordResetToken(string email)
        {
            User user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return StandardResponse<string>.Failed("User not found", 500);
            }

           // return await _userManager.GeneratePasswordResetTokenAsync(user);
           var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return StandardResponse<string>.Success("Success", token, 201);
        }

        public async Task<string> ChangePassword(string email, ChangePasswordRequestDto requestDto)
        {
            User user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return "User not found";
            }

            IdentityResult result = await _userManager.ChangePasswordAsync(user, requestDto.OldPassword, requestDto.NewPassword);

            if (result.Succeeded)
            {
                return "Password changed successfully";
            }
            else
            {
                string errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += error.Description.TrimEnd('.') + ", ";
                }

                return "Password change failed: " + errors.TrimEnd(',', ' ');
            }
        }

        public void SendResetPasswordEmail(string email, string callback_url)
        {
            string logoUrl = "https://res.cloudinary.com/djbkvjfxi/image/upload/v1694601350/uf4xfoda2c4z0exly8nx.png";
            string title = "PharmTech Reset Password";
            string body = $"<html><body><br/><br/>We hope to protect it.<p/>Please click on the link to reset your password. <p/> <a href={callback_url}>Reset Your Password</a> <p/><p/>DropMate is time for DropMate.<p/><br/><br/>With Love from the DropMate Team<p/>Thank you for choosing DropMate.<p/><img src={logoUrl}></body></html>";
            _emailService.SendEmail(email, title, body);
        }
       
            
        

    }
}


