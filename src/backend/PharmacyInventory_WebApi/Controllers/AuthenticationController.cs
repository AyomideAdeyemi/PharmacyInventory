﻿using Microsoft.AspNetCore.Mvc;
using PharmacyInventory_Application.Services.Implementations;
using PharmacyInventory_Application.Services.Interfaces;
using PharmacyInventory_Domain.Dtos;
using PharmacyInventory_Domain.Dtos.Requests;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PharmacyInventory_WebApi.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
      private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;  
        }

        [HttpPost("ContactUs")]
        public IActionResult SendContactMessage([FromForm] ContactUs contact)
        {
            _authenticationService.SendContactMessage(contact);
            return Ok("Message sent successfully!");
        }

        /// <summary>
        /// Description: This EndPoint Register an ordinary user
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>


        [HttpPost("register")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<string>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]

        public async Task<IActionResult> RegisterUser([FromForm] UserRequestDto requestDto)
        {
            var response = await _authenticationService.RegisterUser(requestDto);
            if (response.Succeeded)
            {
                var token = response.Data;
                string encodedToken = System.Text.Encodings.Web.UrlEncoder.Default.Encode(token);
                string callback_url = Request.Scheme + "://" + Request.Host + $"/api/authentication/confirm-email/{requestDto.Email}/{encodedToken}";

                _authenticationService.SendConfirmationEmail(requestDto.Email, callback_url);
                return StatusCode(201, "Account created successfully. Please confirm your email");
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Description: This EndPoint Register an Admin
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        [HttpPost("registerAdmin")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<string>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]

        public async Task<IActionResult> RegisterAdmin([FromForm] UserRequestDto requestDto)
        {
            var response = await _authenticationService.RegisterAdmin(requestDto);
            if (response.Succeeded)
            {
                var token = response.Data;
                string encodedToken = System.Text.Encodings.Web.UrlEncoder.Default.Encode(token);
                string callback_url = Request.Scheme + "://" + Request.Host + $"/api/authentication/confirm-email/{requestDto.Email}/{encodedToken}";

                _authenticationService.SendConfirmationEmail(requestDto.Email, callback_url);
                return StatusCode(201, "Account created successfully. Please confirm your email");
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Description: This EndPoint Register an already registered User to an Admin
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("promote-to-admin/{userId}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<string>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> PromoteToAdmin(string userId)
        {
            var result = await _authenticationService.PromoteToAdminAsync(userId);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Description: This EndPoint To login
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]

        public async Task<IActionResult> LoginUser([FromBody] UserLoginDto requestDto)
        {
            if (!await _authenticationService.ValidateUser(requestDto))
            {
                return Unauthorized();
            }
            return Ok(new { token = await _authenticationService.CreateToken() });
        }
        /// <summary>
        /// Description: This EndPoint activate your  account  through your email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet("Activate-email/{email}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<string>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]

        public async Task<IActionResult> ActivateEmail(string email)
        {
            string token = await _authenticationService.GenerateEmailActivationToken(email);

            string encodedToken = System.Text.Encodings.Web.UrlEncoder.Default.Encode(token);
            string callback_url = Request.Scheme + "://" + Request.Host + $"/api/authentication/confirm-email/{email}/{encodedToken}";

            _authenticationService.SendConfirmationEmail(email, callback_url);
            return StatusCode(200, "Email verification successfully sent. Please confirm your email");

        }
        /// <summary>
        /// Description: This EndPoint Confirm your email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("confirm-email/{email}/{token}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<string>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]

        public async Task<ContentResult> ConfirmEmail(string email, string token)
        {
            string decodedToken = WebUtility.UrlDecode(token);
            await _authenticationService.ConfirmEmailAddress(email, decodedToken);
            string htmlContent = @"
                <!DOCTYPE html>
                    <html lang=""en"">
                    <head>
                        <meta charset=""UTF-8"">
                        <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                        <title>Email Verified</title>
                        <style>
                            /* Center the verification container */
                            body {
                                display: flex;
                                justify-content: center;
                                align-items: center;
                                height: 100vh;
                                margin: 0;
                            }

                            /* Style for the white background with shadow */
                            .verification-container {
                                background-color: #ffffff;
                                padding: 20px;
                                text-align: center;
                                border-radius: 5px;
                                box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
                            }

                            /* Style for the checkmark icon */
                            .checkmark {
                                font-size: 48px;
                                color: #00cc00; /* Green color for the checkmark */
                            }

                            /* Style for the ""Email Verified"" text */
                            .verified-text {
                                font-size: 24px;
                                color: #333333;
                                margin-top: 10px;
                            }/* Style for the ""Welcome to PharmTech Delivery"" text */
                            .dropmate-text {
                                font-size: 28px;
                                color: #333333;
                                margin-top: 10px;
                                font-weight: bold;
                            }
                        </style>
                    </head>
                    <body>
                        <div class=""verification-container"">
                            <!-- Checkmark icon -->
                            <div class=""checkmark"">&#10003;</div>
        
                            <!-- ""Email Verified"" text -->
                            <div class=""verified-text"">Verified Successfully</div>
        
                            <!-- ""Welcome to PharmTech Delivery"" text -->
                            <div class=""pharmacy-text"">Welcome to PharmTech </div>
                        </div>
                    </body>
                    </html>";
            return new ContentResult
            {
                Content = htmlContent,
                ContentType = "text/html"
            };
        }

        //[HttpGet("forget-password/{email}")]
        //public async Task<IActionResult> ForgetPassword(string email)
        //{
        //    string resetToken = await _authenticationService.GeneratePasswordResetToken(email);

        //    string encodedToken = System.Text.Encodings.Web.UrlEncoder.Default.Encode(resetToken);

        //    //Change to call the frontend url for entering new password and resetting with this resetToken passed in the header
        //    string callback_url = Request.Scheme + "://" + Request.Host + $"/api/authentication/confirm-email/{email}/{encodedToken}";//currently backend url

        //    _authenticationService.SendResetPasswordEmail(email, callback_url);
        //    return StatusCode(200, "Password reset successfully sent to your email.");

        //}

        //[HttpGet("reset-password/{token}")]
        //public async Task<IActionResult> ResetPassword(string token, [FromBody] UserLoginDto requestDto)
        //{
        //    string decodedToken = WebUtility.UrlDecode(token);

        //    await _authenticationService.ResetPassword(decodedToken, requestDto);
        //    return Ok("Your password has been reset successfully");
        //}
        
        //[HttpGet("change-password")]
        ////[Authorize]
        //public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestDto requestDto)
        //{
        //    var claimsIdentity = User.Identity as ClaimsIdentity;
        //    var userNameClaim = claimsIdentity.FindFirst(ClaimTypes.Name);
        //    string email = userNameClaim.Value;
        //    await _authenticationService.ChangePassword(email, requestDto);
        //    return Ok("Your password has been changed successfully");
        //}

    }
}

 