using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacyInventory_Application.Services.Interfaces;
using PharmacyInventory_Domain.Dtos.Requests;
using PharmacyInventory_Shared.RequestParameter.ModelParameters;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PharmacyInventory_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var result = await _userService.GetUserById(id);
            return Ok(result);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserRequestDto requestDto)
        {
            var result = await _userService.CreateUserAsync(requestDto);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUser([FromQuery] UserRequestInputParameter parameter)
        {
            var result = await _userService.GetAllUsers(parameter);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.Data.Item2));
            return Ok(result.Data.Item1);
        }

        // PUT api/<DrugController>/5
        [HttpPut("{UpdateUser}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserRequestDto requestDto)
        {
            var result = await _userService.UpdateUser(id, requestDto);
            return Ok(result);
        }

        // DELETE api/<DrugController>/5
        [HttpDelete("{DeleteUser}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUser(id);
            return Ok(result);
        }
    }

}

