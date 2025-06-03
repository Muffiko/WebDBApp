using Microsoft.AspNetCore.Mvc;
using RepairManagementSystem.Models.DTOs;
using RepairManagementSystem.Services.Interfaces;

namespace RepairManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class RepairObjectTypeController : ControllerBase
    {
        private readonly IRepairObjectTypeService _repairObjectTypeService;
        public RepairObjectTypeController(IRepairObjectTypeService repairObjectTypeService)
        {
            _repairObjectTypeService = repairObjectTypeService;
        }
        [HttpGet]
        public async Task<IActionResult> GetRepairObjectTypes()
        {
            var repairObjectTypes = await _repairObjectTypeService.GetAllRepairObjectTypesAsync();
            return Ok(repairObjectTypes);
        }
        [HttpGet("{repairObjectTypeId}")]
        public async Task<IActionResult> GetRepairObjectType(string repairObjectTypeId)
        {
            var repairObjectType = await _repairObjectTypeService.GetRepairObjectTypeByIdAsync(repairObjectTypeId);
            if (repairObjectType == null)
                return NotFound($"Repair object type with ID {repairObjectTypeId} not found.");
            return Ok(repairObjectType);
        }
        [HttpPost]
        public async Task<IActionResult> AddRepairObjectType([FromBody] RepairObjectTypeDTO repairObjectTypeDTO)
        {
            var result = await _repairObjectTypeService.AddRepairObjectTypeAsync(repairObjectTypeDTO);
            if (result == null)
                return BadRequest("Repair object type cannot be null or invalid.");
            return Ok(result);
        }
        [HttpPut("{repairObjectTypeId}")]
        public async Task<IActionResult> UpdateRepairObjectType(string repairObjectTypeId, [FromBody] RepairObjectTypeDTO updatedRepairObjectType)
        {
            var result = await _repairObjectTypeService.UpdateRepairObjectTypeAsync(repairObjectTypeId, updatedRepairObjectType);
            if (result == null)
                return NotFound($"Repair object type with ID {repairObjectTypeId} not found.");
            return Ok($"Repair object type with ID {repairObjectTypeId} updated successfully.");
        }
        [HttpDelete("{repairObjectTypeId}")]
        public async Task<IActionResult> DeleteRepairObjectType(string repairObjectTypeId)
        {
            var result = await _repairObjectTypeService.DeleteRepairObjectTypeAsync(repairObjectTypeId);
            if (result == null)
                return NotFound($"Repair object type with ID {repairObjectTypeId} not found.");
            return Ok($"Repair object type with ID {repairObjectTypeId} deleted successfully.");
        }
        [HttpGet("names")]
        public async Task<IActionResult> GetAllRepairObjectNames()
        {
            var repairObjectNames = await _repairObjectTypeService.GetAllRepairObjectNameAsync();
            return Ok(repairObjectNames);
        }
    }
}
