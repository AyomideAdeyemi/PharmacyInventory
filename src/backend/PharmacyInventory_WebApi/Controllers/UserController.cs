using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacyInventory_Application.Services.Interfaces;
using PharmacyInventory_Domain.Dtos;
using PharmacyInventory_Domain.Dtos.Requests;
using PharmacyInventory_Domain.Dtos.Responses;
using PharmacyInventory_Shared.RequestParameter.ModelParameters;
using Swashbuckle.AspNetCore.Annotations;
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
        /// <summary>
        /// Description: This EndPoint retrieve a user by its Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<UserResponseDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]

        public async Task<IActionResult> GetUserById(string id)
        {
            var result = await _userService.GetUserById(id);
            return Ok(result);
        }

       
        /// <summary>
        /// Description: This EndPoint retrieves all user from the database.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<IEnumerable<UserResponseDto>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]

        public async Task<IActionResult> GetAllUser([FromQuery]UserRequestInputParameter parameter)
        {
            var result = await _userService.GetAllUsersAsync(parameter);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.Data.MetaData));
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Description: This EndPoint update a user by its Id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        [HttpPut("UpdateUser{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<UserResponseDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]

        public async Task<IActionResult> UpdateUser(string id, [FromForm] UserRequestDto requestDto)
        {
            var result = await _userService.UpdateUser(id, requestDto);
            return Ok(result);
        }

        /// <summary>
        /// Description: This EndPoint delete a user by its Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteUser{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<UserResponseDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]

        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _userService.DeleteUser(id);
            return Ok(result);
        }
    }

}

