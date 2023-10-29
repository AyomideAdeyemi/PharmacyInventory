using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacyInventory_Application.Services.Interfaces;
using PharmacyInventory_Domain.Dtos;
using PharmacyInventory_Domain.Dtos.Requests;
using PharmacyInventory_Domain.Dtos.Responses;
using PharmacyInventory_Shared.RequestParameter.ModelParameter;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PharmacyInventory_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        /// <summary>
        /// Description: This EndPoint get a brand by its Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<BrandResponseDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]

        public async Task<IActionResult> GetBrandById(string id)
        {
            var result = await _brandService.GetBrandById(id);
            return Ok(result);
        }
        /// <summary>
        /// Description: This EndPoint retrieve all brands from database.
        /// </summary>
        /// <returns>aA List of all brands</returns>
       
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<IEnumerable<BrandResponseDto>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]

        public async Task<IActionResult> GetAllBrands([FromQuery]BrandRequestInputParameter parameter)
        {
            var result = await _brandService.GetAllBrands(parameter);
           Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.Data.MetaData));
            return StatusCode(result.StatusCode, result);
        }

       
        /// <summary>
        /// Description: This EndPoint create a new Brand.
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<BrandResponseDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]

        public async Task<IActionResult> CreateBrand([FromForm] BrandRequestDto requestDto)
        {
            var result = await _brandService.CreateBrandAsync(requestDto);
            return Ok(result);
        }
       
        /// <summary>
        /// Description: This EndPoint update a brand by its Id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        [HttpPut("UpdateBrand/{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<BrandResponseDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]

        public async Task<IActionResult> UpdateBrand(string id, [FromBody] BrandRequestDto requestDto)
        {
            var result = await _brandService.UpdateBrand(id, requestDto);
            return Ok(result);
        }

        /// <summary>
        /// Description: This EndPoint deletes a brand by its Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteBrand{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<BrandResponseDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]

        public async Task<IActionResult> DeleteBrand(string id)
        {
            var result = await _brandService.DeleteBrand(id);
            return Ok(result);
        }
    }
}
