using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacyInventory_Application.Services.Implementations;
using PharmacyInventory_Application.Services.Interfaces;
using PharmacyInventory_Domain.Dtos.Requests;
using PharmacyInventory_Shared.RequestParameter.ModelParameter;
using PharmacyInventory_Shared.RequestParameter.ModelParameters;
using System.Data;
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

        // GET api/<DrugController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBrandById(string id)
        {
            var result = await _brandService.GetBrandById(id);
            return Ok(result);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllBrands()
        {
            var result = await _brandService.GetAllBrands();
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.Data.Item2));
            return Ok(result.Data.Item1);
        }

        // POST api/<DrugController>
        [HttpPost]
        public async Task<IActionResult> CreateBrand([FromForm] BrandRequestDto requestDto)
        {
            var result = await _brandService.CreateBrandAsync(requestDto);
            return Ok(result);
        }
       
        // PUT api/<DrugController>/5
        [HttpPut("UpdateBrand/{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateBrand(string id, [FromBody] BrandRequestDto requestDto)
        {
            var result = await _brandService.UpdateBrand(id, requestDto);
            return Ok(result);
        }

        // DELETE api/<DrugController>/5
        [HttpDelete("DeleteBrand{id}")]
       // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBrand(string id)
        {
            var result = await _brandService.DeleteBrand(id);
            return Ok(result);
        }
    }
}
