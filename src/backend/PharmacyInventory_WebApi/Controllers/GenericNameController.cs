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
    public class GenericNameController : ControllerBase
    {
        private readonly IGenericNameService _genericNameService;

        public GenericNameController(IGenericNameService genericNameService)
        {
            _genericNameService = genericNameService;
        }
        // GET api/<DrugController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGenericNameById(int id)
        {
            var result = await _genericNameService.GetGenericNameById(id);
            return Ok(result);
        }

        // GET: api/<GenericNameController>
        [HttpGet]
        public async Task<IActionResult> GetAllGenericName([FromQuery] GenericNameRequestInputParameter parameter)
        {
            var result = await _genericNameService.GetAllUGenericName(parameter);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.Data.Item2));
            return Ok(result.Data.Item1);
        }

        // POST api/<DrugController>
        [HttpPost]
        public async Task<IActionResult> CreateGenericName([FromBody] GenericNameRequestDto requestDto)
        {
            var result = await _genericNameService.CreateGenericNameAsync(requestDto);
            return Ok(result);
        }

        // PUT api/<DrugController>/5
        [HttpPut("{UpdateGenericName}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateGenericName(int id, [FromBody] GenericNameRequestDto requestDto)
        {
            var result = await _genericNameService.UpdateGenericName(id, requestDto);
            return Ok(result);
        }

        // DELETE api/<DrugController>/5
        [HttpDelete("{DeleteGenericName}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteGenericName(int id)
        {
            var result = await _genericNameService.DeleteGenericName(id);
            return Ok(result);
        }
    }

}

