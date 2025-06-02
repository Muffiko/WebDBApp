using Microsoft.AspNetCore.Mvc;
using RepairManagementSystem.Models.DTOs;
using RepairManagementSystem.Services.Interfaces;

namespace RepairManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class RepairActivityController : ControllerBase
    {
        private readonly IRepairActivityService _repairActivityService;
        public RepairActivityController(IRepairActivityService repairActivityService)
        {
            _repairActivityService = repairActivityService;
        }
        [HttpGet]
        public async Task<IActionResult> GetRepairActivities()
        {
            var repairActivities = await _repairActivityService.GetAllRepairActivitiesAsync();
            return Ok(repairActivities);
        }
        [HttpGet("{repairActivityId:int}")]
        public async Task<IActionResult> GetRepairActivity(int repairActivityId)
        {
            var repairActivity = await _repairActivityService.GetRepairActivityByIdAsync(repairActivityId);
            if (repairActivity == null)
                return NotFound($"Repair activity with ID {repairActivityId} not found.");
            return Ok(repairActivity);
        }
        [HttpPost]
        public async Task<IActionResult> AddRepairActivity([FromBody] RepairActivityDTO repairActivityDTO)
        {
            var result = await _repairActivityService.AddRepairActivityAsync(repairActivityDTO);
            if (result == null)
                return BadRequest("Repair activity cannot be null or invalid.");
            return Ok(result);
        }
        [HttpPut("{repairActivityId:int}")]
        public async Task<IActionResult> UpdateRepairActivity(int repairActivityId, [FromBody] RepairActivityDTO updatedRepairActivity)
        {
            var result = await _repairActivityService.UpdateRepairActivityAsync(repairActivityId, updatedRepairActivity);
            if (result == null)
                return NotFound($"Repair activity with ID {repairActivityId} not found.");
            return Ok($"Repair activity with ID {repairActivityId} updated successfully.");
        }
        [HttpDelete("{repairActivityId:int}")]
        public async Task<IActionResult> DeleteRepairActivity(int repairActivityId)
        {
            var result = await _repairActivityService.DeleteRepairActivityAsync(repairActivityId);
            if (result == null)
                return NotFound($"Repair activity with ID {repairActivityId} not found.");
            return Ok($"Repair activity with ID {repairActivityId} deleted successfully.");
        }
    }
}
