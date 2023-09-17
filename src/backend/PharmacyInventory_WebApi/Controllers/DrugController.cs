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
    public class DrugController : ControllerBase
    {
        private readonly IDrugService _drugService;

        public DrugController(IDrugService drugService)
        {
            _drugService = drugService;
        }
        // GET: api/<DrugController>
        [HttpGet]

        public async Task<IActionResult> GetAllDrugs()
        {
            var result = await _drugService.GetAllDrugs();
           Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.Data.Item2));
            return Ok(result.Data.Item1);
        }

        [HttpGet("expiry-range")]
        public async Task<IActionResult> GetDrugsByExpiryDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var result = await _drugService.GetDrugsByExpiryDateRange(startDate, endDate);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.Data.Item2));
            return Ok(result.Data.Item1);

        }


        // GET: api/<DrugController>
        [HttpGet("brand/{brandId}")]
        public async Task<IActionResult> GetDrugsByBrand(string brandId)
        {
            var result = await _drugService.GetDrugsByBrand(brandId);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.Data.Item2));
            return Ok(result.Data.Item1);
        }

        // GET: api/<DrugController>
        [HttpGet("supplier/{supplierId}")]
        public async Task<IActionResult> GetDrugsBySupplier(string supplierId)
        {
            var result = await _drugService.GetDrugsBySupplier(supplierId);
            Response.Headers.Add("X-Pagination",JsonSerializer.Serialize(result.Data.Item2));
            return Ok(result.Data.Item1);
        }

        // GET: api/<DrugController>
        [HttpGet("genericName/{genericNameId}")]
        public async Task<IActionResult> GetDrugsByGenericName(string genericNameId)
        {
            var result = await _drugService.GetDrugsByGenericName(genericNameId);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.Data.Item2));
            return Ok(result.Data.Item1);
            
        }



        // GET api/<DrugController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDrugById(string id)
        {
            var result = await _drugService.GetDrugById(id);
            return Ok(result);
        }

        // POST api/<DrugController>
        [HttpPost]
        public async Task<IActionResult> CreateDrug([FromForm] DrugRequestDto requestDto)
        {
            var result = await _drugService.CreateDrugAsync(requestDto);
            return Ok(result);
        }

        // PUT api/<DrugController>/5
        [HttpPut("UpdateDrug/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateDrug(string id, [FromBody] DrugRequestDto requestDto)
        {
            var result = await _drugService.UpdateDrug(id, requestDto);
            return Ok(result);
        }

        // DELETE api/<DrugController>/5
        [HttpDelete("DeleteDrugs/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteDrug(string id)
        {
            var result = await _drugService.DeleteDrug(id);
            return Ok(result);
        }

        //[HttpPost("check-low-quantity")]
        //public async Task<IActionResult> CheckLowQuantityNotificationsAsync()
        //{
        //    await _drugService.CheckAndSendLowQuantityNotificationsAsync();
        //    return Ok("Low quantity notifications sent.");
        //}

        //[HttpPost("check-expiring-drugs")]
        //public async Task<IActionResult> CheckExpiringDrugNotificationsAsync()
        //{
        //    await _drugService.CheckAndSendExpiringDrugNotificationsAsync();
        //    return Ok("Expiring drug notifications sent.");
        //}
    }
}
