using Microsoft.AspNetCore.Mvc;
using RepairManagementSystem.Extensions;
using RepairManagementSystem.Helpers;
using RepairManagementSystem.Models.DTOs;
using RepairManagementSystem.Services.Interfaces;

namespace RepairManagementSystem.Controllers
{
    [ApiController]
    [Route("api/RepairActivities")]
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
            return Ok(repairActivity);
        }
        [HttpPost]
        public async Task<IActionResult> AddRepairActivity([FromBody] RepairActivityDTO repairActivityDTO)
        {
            var result = await _repairActivityService.AddRepairActivityAsync(repairActivityDTO);
            return this.ToApiResponse(result);
        }
        [HttpPut("{repairActivityId:int}")]
        public async Task<IActionResult> UpdateRepairActivity(int repairActivityId, [FromBody] RepairActivityDTO updatedRepairActivity)
        {
            var result = await _repairActivityService.UpdateRepairActivityAsync(repairActivityId, updatedRepairActivity);
            return this.ToApiResponse(result);
        }
        [HttpDelete("{repairActivityId:int}")]
        public async Task<IActionResult> DeleteRepairActivity(int repairActivityId)
        {
            var result = await _repairActivityService.DeleteRepairActivityAsync(repairActivityId);
            return this.ToApiResponse(result);
        }
    }
}