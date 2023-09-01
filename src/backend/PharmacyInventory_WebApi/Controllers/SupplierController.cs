using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacyInventory_Application.Services.Implementations;
using PharmacyInventory_Application.Services.Interfaces;
using PharmacyInventory_Domain.Dtos.Requests;
using PharmacyInventory_Shared.RequestParameter.ModelParameters;
using System.Data;
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
        // GET api/<DrugController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSupplierById(int id)
        {
            var result = await _supplierService.GetSupplierById(id);
            return Ok(result);
        }

        // POST api/<DrugController>
        [HttpPost]
        public async Task<IActionResult> CreateSupplier([FromBody] SupplierRequestDto requestDto)
        {
            var result = await _supplierService.CreateSupplierAsync(requestDto);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSupplier([FromQuery] SupplierRequestInputParameter parameter)
        {
            var result = await _supplierService.GetAllSuppliers(parameter);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.Data.Item2));
            return Ok(result.Data.Item1);
        }

        // PUT api/<DrugController>/5
        [HttpPut("{UpdateSupplier}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateSupplier(int id, [FromBody] SupplierRequestDto requestDto)
        {
            var result = await _supplierService.UpdateSupplier(id, requestDto);
            return Ok(result);
        }

        // DELETE api/<DrugController>/5
        [HttpDelete("{DeleteSupplier}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            var result = await _supplierService.DeleteSupplier(id);
            return Ok(result);
        }
    }
    
    
}
