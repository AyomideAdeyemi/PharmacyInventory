using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacyInventory_Application.Services.Interfaces;
using PharmacyInventory_Domain.Dtos;
using PharmacyInventory_Domain.Dtos.Requests;
using PharmacyInventory_Domain.Dtos.Responses;
using PharmacyInventory_Shared.RequestParameter.ModelParameters;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PharmacyInventory_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly IUnitService _unitService;

        public UnitController(IUnitService unitService)
        {
            _unitService = unitService;
        }
        /// <summary>
        /// Description: get a unit by its Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<UnitResponseDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]

        public async Task<IActionResult> GetUnitById(string id)
        {
            var result = await _unitService.GetUnitById(id);
            return Ok(result);
        }

        /// <summary>
        /// Description: This EndPoint create a new unit.
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<UnitResponseDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]

        public async Task<IActionResult> CreateUnit([FromForm] UnitRequestDto requestDto)
        {
            var result = await _unitService.CreateUnitAsync(requestDto);
            return Ok(result);
        }

        /// <summary>
        /// Description: This EndPoint update a new unit by its Id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        [HttpPut("UpdateUnit{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<UnitResponseDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]

        public async Task<IActionResult> UpdateUnit(string id, [FromBody] UnitRequestDto requestDto)
        {
            var result = await _unitService.UpdateUnit(id, requestDto);
            return Ok(result);
        }

        /// <summary>
        /// Description: This EndPoint delete a unit by its Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteUnit/{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<UnitResponseDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]

        public async Task<IActionResult> DeleteUnit(string id)
        {
            var result = await _unitService.DeleteUnit(id);
            return Ok(result);
        }
    }
}