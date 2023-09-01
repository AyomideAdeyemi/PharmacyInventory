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

        public async Task<IActionResult> GetAllDrugs([FromQuery] DrugRequestInputParameter parameter)
        {
            var result = await _drugService.GetAllDrugs(parameter);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.Data.Item2));
            return Ok(result.Data.Item1);
        }

        [HttpGet("expiry-range")]
        public async Task<IActionResult> GetDrugsByExpiryDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] DrugRequestInputParameter parameter)
        {
            var result = await _drugService.GetDrugsByExpiryDateRange(startDate, endDate, parameter);
            return Ok(result);
        }


        // GET: api/<DrugController>
        [HttpGet("brand/{brand}")]
        public async Task<IActionResult> GetDrugsByBrand(string brand, [FromQuery] DrugRequestInputParameter parameter)
        {
            var result = await _drugService.GetDrugsByBrand(brand, parameter);
            return Ok(result);
        }

        // GET: api/<DrugController>
        [HttpGet("supplier/{supplier}")]
        public async Task<IActionResult> GetDrugsBySupplier(string supplier, [FromQuery] DrugRequestInputParameter parameter)
        {
            var result = await _drugService.GetDrugsBySupplier(supplier, parameter);
            return Ok(result);
        }

        // GET: api/<DrugController>
        [HttpGet("genericName/{genericName}")]
        public async Task<IActionResult> GetDrugsByGenericName(string genericName, [FromQuery] DrugRequestInputParameter parameter)
        {
            var result = await _drugService.GetDrugsByGenericName(genericName, parameter);
            return Ok(result);
        }



        // GET api/<DrugController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDrugById(int id)
        {
            var result = await _drugService.GetDrugById(id);
            return Ok(result);
        }

        // POST api/<DrugController>
        [HttpPost]
        public async Task<IActionResult> CreateDrug([FromBody] DrugRequestDto requestDto)
        {
            var result = await _drugService.CreateDrugAsync(requestDto);
            return Ok(result);
        }

        // PUT api/<DrugController>/5
        [HttpPut("{UpdateDrug}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateDrug(int id, [FromBody] DrugRequestDto requestDto)
        {
            var result = await _drugService.UpdateDrug(id, requestDto);
            return Ok(result);
        }

        // DELETE api/<DrugController>/5
        [HttpDelete("{DeleteDrugs}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteDrug(int id)
        {
            var result = await _drugService.DeleteDrug(id);
            return Ok(result);
        }
    }
}
