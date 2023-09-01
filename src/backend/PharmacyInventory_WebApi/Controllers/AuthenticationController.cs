using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRequestDto requestDto)
        {
            var result = await _authenticationService.RegisterUser(requestDto);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            return StatusCode(201);
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
