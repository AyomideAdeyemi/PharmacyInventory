using FluentAssertions.Common;
using Microsoft.AspNetCore.Mvc;
using PharmacyInventory_Application.Services.Implementations;
using PharmacyInventory_Application.Services.Interfaces;
using PharmacyInventory_Domain.Dtos.Requests;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PharmacyInventory_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
      private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;  
        }

        //[HttpPost("register-user")]
        //public async Task<IActionResult> RegisterUser([FromForm] UserRequestDto userRequestDto)
        //{
        //    var result = await _authenticationService.RegisterUser(userRequestDto);

        //    if (result.Succeeded)
        //    {
        //        return Ok(result);
        //    }

        //    return BadRequest(result);
        //}
        //[HttpPost("register")]
        //public async Task<IActionResult> RegisterUser([FromForm] UserRequestDto userRequestDto)
        //{
        //    try
        //    {
        //        var emailConfirmationToken = await _authenticationService.RegisterUser(userRequestDto);
        //        return Ok(new { EmailConfirmationToken = emailConfirmationToken });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"An error occurred: {ex.Message}");
        //    }
        //}
        //[HttpPost("register")]
        //public async Task<IActionResult> RegisterUser([FromForm] UserRequestDto userRequestDto)
        //{
        //    var emailConfirmationToken = await _authenticationService.RegisterUser(userRequestDto);

        //    if (emailConfirmationToken != null)
        //    {
        //        // Registration successful, you can return the email confirmation token or any other response.
        //        return Ok(new { EmailConfirmationToken = emailConfirmationToken });
        //    }

        //    // Registration failed, handle the error and return an appropriate response.
        //    return BadRequest("User registration failed."); // You can customize this response message.
        //}

        //[HttpPost("register")]
        //public async Task<IActionResult> RegisterUser([FromForm] UserRequestDto userRequestDto)
        //{
        //    var result = await _authenticationService.RegisterUser(userRequestDto);

        //    if (result.Succeeded)
        //    {
        //        return Ok("User registered successfully");
        //    }

        //    return BadRequest(result.Errors);
        //}

        [HttpPost("register")]
       // [ServiceFilter(typeof(ValidationActionFilters))]
        public async Task<IActionResult> RegisterUser([FromForm] UserRequestDto requestDto)
        {
            string token = await _authenticationService.RegisterUser(requestDto);

            string encodedToken = System.Text.Encodings.Web.UrlEncoder.Default.Encode(token);
            string callback_url = Request.Scheme + "://" + Request.Host + $"/api/authentication/confirm-email/{requestDto.Email}/{encodedToken}";

            _authenticationService.SendConfirmationEmail(requestDto.Email, callback_url);
            return StatusCode(201, "Account created successfully. Please confirm your email");
        }

        [HttpPost("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromForm] UserRequestDto userRequestDto)
        {
            var result = await _authenticationService.RegisterAdmin(userRequestDto);

            if (result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return Ok("Admin registered successfully");
            }

            return BadRequest(result.Errors);
        }
    
    [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginDto requestDto)
        {
            if (!await _authenticationService.ValidateUser(requestDto))
            {
                return Unauthorized();
            }
            return Ok(new { token = await _authenticationService.CreateToken() });
        }
    }
}
