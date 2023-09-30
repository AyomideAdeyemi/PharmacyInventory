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
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }
        /// <summary>
        /// Description: This EndPoint Retrieves a supplier by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The supplier details </returns>
        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<SupplierResponseDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]

        public async Task<IActionResult> GetSupplierById(string id)
        {
            var result = await _supplierService.GetSupplierById(id);
            return Ok(result);
        }

        /// <summary>
        /// Description: This EndPoint Create a new Supplier.
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns>The newly created supplier</returns>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<SupplierResponseDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]

        public async Task<IActionResult> CreateSupplier([FromForm] SupplierRequestDto requestDto)
        {
            var result = await _supplierService.CreateSupplierAsync(requestDto);
            return Ok(result);
        }

        /// <summary>
        /// Description: This EndPoint Retrieve all supplier from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<IEnumerable<SupplierResponseDto>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]

        public async Task<IActionResult> GetAllSupplier([FromQuery]SupplierRequestInputParameter parameter)
        {
            var result = await _supplierService.GetAllSuppliers(parameter);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.Data.MetaData));
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Description: This EndPoint Update supplier details by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="requestDto"></param>
        /// <returns>A message indicating the result of the operation</returns>
        [HttpPut("UpdateSupplier{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<SupplierResponseDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]

        public async Task<IActionResult> UpdateSupplier(string id, [FromBody] SupplierRequestDto requestDto)
        {
            var result = await _supplierService.UpdateSupplier(id, requestDto);
            return Ok(result);
        }

        /// <summary>
        /// Description: This EndPoint delete a user by its Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteSupplier{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<SupplierResponseDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]

        public async Task<IActionResult> DeleteSupplier(string id)
        {
            var result = await _supplierService.DeleteSupplier(id);
            return Ok(result);
        }
    }
    
    
}
