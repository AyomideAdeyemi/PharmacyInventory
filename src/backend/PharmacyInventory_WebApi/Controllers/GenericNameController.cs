using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacyInventory_Application.Services.Interfaces;
using PharmacyInventory_Domain.Dtos;
using PharmacyInventory_Domain.Dtos.Requests;
using PharmacyInventory_Domain.Dtos.Responses;
using PharmacyInventory_Shared.RequestParameter.ModelParameters;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json;


namespace PharmacyInventory_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericNameController : ControllerBase
    {
        private readonly IGenericNameService _genericNameService;

        public GenericNameController(IGenericNameService genericNameService)
        {
            _genericNameService = genericNameService;
        }

        /// <summary>
        /// Description: This EndPoint Retrieves a particular genericName by its Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
       [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<GenericNameResponseDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]

        public async Task<IActionResult> GetGenericNameById(string id)
        {
            var result = await _genericNameService.GetGenericNameById(id);
            return Ok(result);
        }

        /// <summary>
        /// Description:This EndPoint Retrieves all genericName from database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<IEnumerable<GenericNameResponseDto>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> GetAllGenericName([FromQuery]GenericNameRequestInputParameter parameter)
        {
            var result = await _genericNameService.GetAllGenericName(parameter);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.Data.MetaData));
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Description: This EndPoint Create a new GenericName
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns> The newly created genericName</returns>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<GenericNameResponseDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]

        public async Task<IActionResult> CreateGenericName([FromForm] GenericNameRequestDto requestDto)
        {
            var result = await _genericNameService.CreateGenericNameAsync(requestDto);
            return Ok(result);
        }

        /// <summary>
        /// Description:This EndPoint Update a genericName by its Id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        [HttpPut("UpdateGenericName{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<GenericNameResponseDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]

        public async Task<IActionResult> UpdateGenericName(string id, [FromBody] GenericNameRequestDto requestDto)
        {
            var result = await _genericNameService.UpdateGenericName(id, requestDto);
            return Ok(result);
        }

        /// <summary>
        /// Description:This EndPoint Delete a genericName by its Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteGenericName/{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<string>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]

        public async Task<IActionResult> DeleteGenericName(string id)
        {
            var result = await _genericNameService.DeleteGenericName(id);
            return Ok(result);
        }
    }

}

