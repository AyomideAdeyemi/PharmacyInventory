using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PharmacyInventory_Application.Services.Interfaces;
using PharmacyInventory_Domain.Dtos.Requests;
using PharmacyInventory_Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
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

        private User? _user;

        public AuthenticationService(ILogger<AuthenticationService> logger, IMapper mapper, UserManager<User> userManager, IConfiguration configuration, IEmailService emailService)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _emailService = emailService;


        }

       
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            return users;
        }
       
       
public async Task<string> RegisteriUser(UserRequestDto userRequestDto)
        {
            var user = _mapper.Map<User>(userRequestDto);
            user.UserName = user.Email;

            var result = await _userManager.CreateAsync(user, userRequestDto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");

                var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                return emailConfirmationToken;
            }

            return null; // Registration failed, return null or handle the error as needed
        }


        public async Task<string> RegisterUser(UserRequestDto userRequestDto)
        {
            var user = _mapper.Map<User>(userRequestDto);
            user.UserName = user.Email;
            var result = await _userManager.CreateAsync(user, userRequestDto.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");

                // Generate email confirmation token
                ////var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                // Compose email
                //string subject = "Welcome to Pharmacy Inventory";
                //string body = $"Thank you for registering with Pharmacy Inventory. Please confirm your email by clicking the following link: <a href=\"{{confirmationLink}}\">Confirm Email</a>";
                //body = body.Replace("{{confirmationLink}}", $"http://yourdomain.com/confirm-email?email={user.Email}&token={WebUtility.UrlEncode(emailConfirmationToken)}");

                // Send email
               // _emailService.SendEmail(user.Email, subject, body);
            }
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }
    
    public async Task<IdentityResult> RegisterAdmin(UserRequestDto userRequestDto)
        {

            var user = _mapper.Map<User>(userRequestDto);
            user.UserName = user.Email;
            var result = await _userManager.CreateAsync(user, userRequestDto.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Admin");
            }
            return result;
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

        public async Task<string> ConfirmEmailAddress(string email, string token)
        {
            string trimmedToken = token.Replace(" ", "+");

            User user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return "User not found.";
            }

            if (user.EmailConfirmed)
            {
                return "Email is already confirmed.";
            }

            IdentityResult result = await _userManager.ConfirmEmailAsync(user, trimmedToken);

            if (!result.Succeeded)
            {
                return "Error confirming email.";
            }

            return "Email confirmed successfully.";
        }


        public void SendConfirmationEmail(string email, string callback_url)
        {
           // string logoUrl = "https://res.cloudinary.com/djbkvjfxi/image/upload/v1694601350/uf4xfoda2c4z0exly8nx.png";
            string title = "DropMate Confirm Your Email";
          //  string body = $"<html><body><br/><br/>Please click to confirm your email address for DropMate Delivery. When you confirm your email you get full access to DropMate services for free.<p/> <a href={callback_url}>Verify Your Email</a> <p/><br/>DropMate is a game-changing delivery platform designed to simplify your life. Say goodbye to the hassles of traditional delivery services and experience a whole new level of convenience. Whether you need groceries, packages, or your favorite takeout, DropMate connects you with a network of reliable couriers who are ready to pick up and drop off your items with lightning speed. With real-time tracking, secure payments, and a seamless user interface, DropMate ensures that your deliveries are not only efficient but also stress-free. It's time to embrace a smarter way to send and receive goods – it's time for DropMate.<p/><br/><br/>With Love from the DropMate Team<p/>Thank you for choosing DropMate.<p/><img src={logoUrl}></body></html>";
            string body = $"<html><body><br/><br/>Please click to confirmccess to" +
                $" DropMate services for free.<p/> <a href={callback_url}>Verify Your Email</a> <p/><br/>DropMate is  Sace a whole new level ofer way to send and receive goods – it's time for DropMate.<p/><br/><br/>With Love from the DropMate Team<p/>Thank you for choosing DropMate.</body></html>";

            _emailService.SendEmail(email, title, body);
        }
    }
}


