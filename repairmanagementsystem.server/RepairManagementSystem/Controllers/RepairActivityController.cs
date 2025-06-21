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
        private readonly IRepairRequestService _repairRequestService;
        public RepairActivityController(IRepairActivityService repairActivityService, IRepairRequestService repairRequestService)
        {
            _repairActivityService = repairActivityService;
            _repairRequestService = repairRequestService;
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
        public async Task<IActionResult> AddRepairActivity([FromBody] RepairActivityRequest repairActivityRequest)
        {
            int? userId = User.GetUserId();
            if (userId == null)
                return this.ToApiResponse(Result.Fail(401, "User is not authenticated."));

            var repairRequest = await _repairRequestService.GetRepairRequestByIdAsync(repairActivityRequest.RepairRequestId);
            if (repairRequest == null)
                return this.ToApiResponse(Result.Fail(400, "Repair request not found."));

            var result = await _repairActivityService.AddRepairActivityAsync(repairActivityRequest);
            return this.ToApiResponse(result);
        }

        [HttpPut("{repairActivityId:int}")]
        public async Task<IActionResult> UpdateRepairActivity(int repairActivityId, [FromBody] RepairActivityRequest updatedRepairActivity)
        {
            var result = await _repairActivityService.UpdateRepairActivityAsync(repairActivityId, updatedRepairActivity);
            return this.ToApiResponse(result);
        }

        [HttpPatch("{repairActivityId:int}")]
        public async Task<IActionResult> PatchRepairActivity(int repairActivityId, [FromBody] RepairActivityPatchRequest patchRequest)
        {
            var result = await _repairActivityService.PatchRepairActivityAsync(repairActivityId, patchRequest);
            return this.ToApiResponse(result);
        }

        [HttpDelete("{repairActivityId:int}")]
        public async Task<IActionResult> DeleteRepairActivity(int repairActivityId)
        {
            var result = await _repairActivityService.DeleteRepairActivityAsync(repairActivityId);
            return this.ToApiResponse(result);
        }

        [HttpPatch("{repairActivityId:int}/change-status")]
        public async Task<IActionResult> ChangeRepairActivityStatus(int repairActivityId, [FromBody] ChangeRepairActivityStatusRequest request)
        {
            int userId = User.GetUserId() ?? 0;
            if (userId == 0)
            {
                return this.ToApiResponse(Result.Fail(401, "User is not authenticated."));
            }

            var repairActivity = await _repairActivityService.GetRepairActivityByIdAsync(repairActivityId);
            if (repairActivity == null || repairActivity.WorkerId != userId)
            {
                return this.ToApiResponse(Result.Fail(403, "You do not have permission to change the status of this repair activity."));
            }

            var result = await _repairActivityService.ChangeRepairActivityStatusAsync(repairActivityId, request);
            return this.ToApiResponse(result);
        }

        [HttpGet("workers/my")]
        public async Task<IActionResult> GetMyRepairActivities()
        {
            int? userId = User.GetUserId();
            if (userId == null)
                return this.ToApiResponse(Result.Fail(401, "User is not authenticated."));

            var repairActivities = await _repairActivityService.GetRepairActivitiesByWorkerIdAsync(userId.Value);
            return Ok(repairActivities);
        }
    }
}
