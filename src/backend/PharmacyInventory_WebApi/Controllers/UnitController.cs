using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacyInventory_Application.Services.Interfaces;
using PharmacyInventory_Domain.Dtos.Requests;
using PharmacyInventory_Shared.RequestParameter.ModelParameters;
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
        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUnitById(string id)
        {
            var result = await _unitService.GetUnitById(id);
            return Ok(result);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> CreateUnit([FromForm] UnitRequestDto requestDto)
        {
            var result = await _unitService.CreateUnitAsync(requestDto);
            return Ok(result);
        }

        // PUT api/<DrugController>/5
        [HttpPut("UpdateUnit{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUnit(string id, [FromBody] UnitRequestDto requestDto)
        {
            var result = await _unitService.UpdateUnit(id, requestDto);
            return Ok(result);
        }

        // DELETE api/<DrugController>/5
        [HttpDelete("DeleteUnit/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUnit(string id)
        {
            var result = await _unitService.DeleteUnit(id);
            return Ok(result);
        }
    }
}