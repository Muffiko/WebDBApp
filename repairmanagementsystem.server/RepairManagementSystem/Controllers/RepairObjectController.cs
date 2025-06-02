using Microsoft.AspNetCore.Mvc;
using RepairManagementSystem.Models;
using RepairManagementSystem.Models.DTOs;
using RepairManagementSystem.Services.Interfaces;

namespace RepairManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class RepairObjectController : ControllerBase
    {
        private readonly IRepairObjectService _repairObjectService;
        public RepairObjectController(IRepairObjectService repairObjectService)
        {
            _repairObjectService = repairObjectService;
        }
        [HttpGet]
        public async Task<IActionResult> GetRepairObjects()
        {
            var repairObjects = await _repairObjectService.GetAllRepairObjectsAsync();
            return Ok(repairObjects);
        }
        [HttpGet]
        [Route("{repairObjectId:int}")]
        public async Task<IActionResult> GetRepairObject(int repairObjectId)
        {
            var repairObject = await _repairObjectService.GetRepairObjectByIdAsync(repairObjectId);
            if (repairObject == null)
                return NotFound($"Repair object with ID {repairObjectId} not found.");
            return Ok(repairObject);
        }
        [HttpPost]
        public async Task<IActionResult> AddRepairObject([FromBody] RepairObjectDTO repairObjectDTO)
        {
            var result = await _repairObjectService.AddRepairObjectAsync(repairObjectDTO);
            if (result == null)
                return BadRequest("Repair object cannot be null or invalid.");
            return Ok(result);
        }
        [HttpPut]
        [Route("{repairObjectId:int}")]
        public async Task<IActionResult> UpdateRepairObject(int repairObjectId, [FromBody] RepairObjectDTO updatedRepairObject)
        {
            var result = await _repairObjectService.UpdateRepairObjectAsync(repairObjectId, updatedRepairObject);
            if (result == null)
                return NotFound($"Repair object with ID {repairObjectId} not found.");
            return Ok($"Repair object with ID {repairObjectId} updated successfully.");
        }
        [HttpDelete]
        [Route("{repairObjectId:int}")]
        public async Task<IActionResult> DeleteRepairObject(int repairObjectId)
        {
            var result = await _repairObjectService.DeleteRepairObjectAsync(repairObjectId);
            if(result == null)
                return NotFound($"Repair object with ID {repairObjectId} not found.");
            return Ok($"Repair object with ID {repairObjectId} deleted successfully.");
        }
    }
}
