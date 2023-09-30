using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacyInventory_Application.Services.Interfaces;
using PharmacyInventory_Domain.Dtos;
using PharmacyInventory_Domain.Dtos.Requests;
using PharmacyInventory_Domain.Dtos.Responses;
using PharmacyInventory_Shared.RequestParameter.Common;
using PharmacyInventory_Shared.RequestParameter.ModelParameters;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PharmacyInventory_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrugController : ControllerBase
    {
        private readonly IDrugService _drugService;

        public DrugController(IDrugService drugService)
        {
            _drugService = drugService;
        }
        /// <summary>
        /// Description: This EndPoint Get all drugs from the database.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<IEnumerable<DrugResponseDto>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> GetAllDrugs([FromQuery]DrugRequestInputParameter parameter)
        {
            var result = await _drugService.GetAllDrugs(parameter);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.Data.MetaData));

            return StatusCode(result.StatusCode, result);
            
        } 
        /// <summary>
        /// Description: This EndPoint Retrieves drugs within a specified expiry date range.
        /// </summary>
        /// <param name="startDate">The start date of the range.</param>
        /// <param name="endDate">The end date of the range.</param>
        /// <returns>A list of drugs within the specified expiry date range.</returns>

        [HttpGet("expiry-range")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<IEnumerable<DrugResponseDto>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]

        public async Task<IActionResult> GetDrugsByExpiryDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] DrugRequestInputParameter parameter)
        {
            var result = await _drugService.GetDrugsByExpiryDateRange(startDate, endDate, parameter);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.Data.Item2));
            return Ok(result.Data.Item1);

        }
        /// <summary>
        /// Description: This EndPoint Retrieves drugs within a specified quantity range.
        /// </summary>
        /// <param name="minQuantity"></param>
        /// <param name="maxQuantity"></param>
        /// <returns>A list of drugs within the specified quantity range</returns>
        [HttpGet("quantity-range")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<IEnumerable<DrugResponseDto>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]

        public async Task<IActionResult> GetDrugsByQuantityRange([FromQuery] double minQuantity, [FromQuery] double maxQuantity)
        {
            var result = await _drugService.GetDrugsByQuantityRange(minQuantity, maxQuantity);

            if (result.Succeeded)
            {
                return Ok(result.Data);
            }

            return StatusCode(result.StatusCode, result.Message);
        }
        /// <summary>
        /// Description: This EndPoint Retrieves all drugs from that brand by its Id.
        /// </summary>
        /// <param name="brandId"></param>
        /// <returns></returns>
        

        [HttpGet("brand/{brandId}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<IEnumerable<DrugResponseDto>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]

        public async Task<ActionResult<StandardResponse<PagedList<DrugResponseDto>>>> GetDrugsByBrandId(string brandId, [FromQuery] DrugRequestInputParameter parameter)
        {
            var response = await _drugService.GetDrugsByBrandId(brandId, parameter);
            return StatusCode(response.StatusCode, response);
        }
        /// <summary>
        /// Description: This EndPoint Retrieves all drugs from that supplier by its Id.
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns>A list of drugs for the specified supplier</returns>
        [HttpGet("supplier/{supplierId}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<IEnumerable<DrugResponseDto>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]

        public async Task<ActionResult<StandardResponse<PagedList<DrugResponseDto>>>> GetDrugsBySupplierId(string supplierId, [FromQuery] DrugRequestInputParameter parameter)
        {
            var response = await _drugService.GetDrugsBySupplier(supplierId, parameter);
            return StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// Description: This EndPoint Retrieves all drugs from that genericName by its Id.
        /// </summary>
        /// <param name="genericNameId"></param>
        /// <returns>A list of drugs for the specified genericName</returns>
        [HttpGet("genericName/{genericNameId}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<IEnumerable<DrugResponseDto>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]

        public async Task<ActionResult<StandardResponse<PagedList<DrugResponseDto>>>> GetDrugsByGenericName(string genericNameId, DrugRequestInputParameter parameter)
        {
            var response = await _drugService.GetDrugsByGenericName(genericNameId, parameter);
            return StatusCode(response.StatusCode, response);
            
        }



        /// <summary>
        /// Description: This EndPoint Get a drug by its Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The drug with the specific ID.</returns>
        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<DrugResponseDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]

        public async Task<IActionResult> GetDrugById(string id)
        {
            var result = await _drugService.GetDrugById(id);
            return Ok(result);
        }

        /// <summary>
        /// Description: This EndPoint To create a new drug.
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns>The newly created drug.</returns>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<DrugResponseDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]

        public async Task<IActionResult> CreateDrug([FromForm] DrugRequestDto requestDto)
        {
            var result = await _drugService.CreateDrugAsync(requestDto);
            return Ok(result);
        }

        /// <summary>
        /// Description: This EndPoint Update a particular drug by its Id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="requestDto"></param>
        /// <returns>A message indicating the result of the operation.</returns>
        [HttpPut("UpdateDrug/{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<DrugResponseDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]

        public async Task<IActionResult> UpdateDrug(string id, [FromForm] DrugRequestDto requestDto)
        {
            var result = await _drugService.UpdateDrug(id, requestDto);
            return Ok(result);
        }

        /// <summary>
        /// Description: This EndPoint Delete a particular drug by its Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A message indicating the result of the operation.</returns>
        [HttpDelete("DeleteDrugs/{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<DrugResponseDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]

        public async Task<IActionResult> DeleteDrug(string id)
        {
            var result = await _drugService.DeleteDrug(id);
            return Ok(result);
        }

          
    }
}
