using Microsoft.AspNetCore.Mvc;
using RepairManagementSystem.Helpers;
using RepairManagementSystem.Models.DTOs;
using RepairManagementSystem.Services.Interfaces;
using RepairManagementSystem.Extensions;

namespace RepairManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class RepairRequestController : ControllerBase
    {
        private readonly IRepairRequestService _repairRequestService;
        private readonly IRepairObjectService _repairObjectService;
        public RepairRequestController(IRepairRequestService repairRequestService, IRepairObjectService repairObjectService)
        {
            _repairRequestService = repairRequestService;
            _repairObjectService = repairObjectService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRepairRequests()
        {
            var repairRequests = await _repairRequestService.GetAllRepairRequestsAsync();
            return Ok(repairRequests);
        }

        [HttpGet("{repairRequestId:int}")]
        public async Task<IActionResult> GetRepairRequest(int repairRequestId)
        {
            var repairRequest = await _repairRequestService.GetRepairRequestByIdAsync(repairRequestId);
            return Ok(repairRequest);
        }

        [HttpPost]
        public async Task<IActionResult> AddRepairRequest([FromBody] RepairRequestAdd request)
        {
            int? customerId = User.GetUserId();
            if (customerId == null)
            {
                return this.ToApiResponse(Result.Fail(401, "User is not authenticated."));
            }

            var repairObject = await _repairObjectService.GetRepairObjectByIdAsync(request.RepairObjectId);
            if (repairObject == null)
            {
                return this.ToApiResponse(Result.Fail(400, "Repair object not found."));
            }

            if (repairObject.CustomerId != customerId.Value)
            {
                return this.ToApiResponse(Result.Fail(403, "You are not authorized for this repair object."));
            }

            var existingRequest = await _repairRequestService.GetAllRepairRequestsFromCustomerAsync(customerId.Value);
            if (existingRequest?.Any(r => r?.RepairObjectId == request.RepairObjectId) == true)
            {
                return this.ToApiResponse(Result.Fail(400, "You already have a repair request for this object."));
            }

            var result = await _repairRequestService.AddRepairRequestAsync(request);
            return this.ToApiResponse(result);
        }

        [HttpPut("{repairRequestId:int}")]
        public async Task<IActionResult> UpdateRepairRequest(int repairRequestId, [FromBody] RepairRequestDTO updatedRepairRequest)
        {
            var result = await _repairRequestService.UpdateRepairRequestAsync(repairRequestId, updatedRepairRequest);
            return this.ToApiResponse(result);
        }

        [HttpDelete("{repairRequestId:int}")]
        public async Task<IActionResult> DeleteRepairRequest(int repairRequestId)
        {
            var result = await _repairRequestService.DeleteRepairRequestAsync(repairRequestId);
            return this.ToApiResponse(result);
        }

        [HttpGet("customer/{customerId:int}")]
        public async Task<IActionResult> GetAllRepairRequestsFromCustomer(int customerId)
        {
            var repairRequests = await _repairRequestService.GetAllRepairRequestsFromCustomerAsync(customerId);
            return Ok(repairRequests);
        }

        [HttpGet("unassigned")]
        public async Task<IActionResult> GetUnassignedRepairRequests()
        {
            var unassignedRepairRequests = await _repairRequestService.GetUnassignedRepairRequestsAsync();
            return Ok(unassignedRepairRequests);
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActiveRepairRequests()
        {
            var activeRepairRequests = await _repairRequestService.GetActiveRepairRequestsAsync();
            return Ok(activeRepairRequests);
        }

        [HttpGet("customer/my")]
        public async Task<IActionResult> GetMyRepairRequests()
        {
            var customerId = User.GetUserId();
            if (customerId == null)
            {
                return this.ToApiResponse(Result.Fail(401, "User is not authenticated."));
            }
            var repairRequests = await _repairRequestService.GetAllRepairRequestsForCustomerAsync(customerId.Value);
            return Ok(repairRequests);
        }

        [HttpPatch("{repairRequestId:int}/assign")]
        public async Task<IActionResult> AssignRepairRequest(int repairRequestId, [FromBody] RepairRequestAssign request)
        {
            var result = await _repairRequestService.AssignRepairRequestToManagerAsync(repairRequestId, request);
            return this.ToApiResponse(result);
        }

        [HttpPatch("{repairRequestId:int}/change-status")]
        public async Task<IActionResult> ChangeRepairRequestStatus(int repairRequestId, [FromBody] RepairRequestChangeStatusRequest request)
        {
            var result = await _repairRequestService.ChangeRepairRequestStatusAsync(repairRequestId, request);
            return this.ToApiResponse(result);
        }

        [HttpPatch("{repairRequestId:int}/unassign")]
        public async Task<IActionResult> UnassignRepairRequestManager(int repairRequestId)
        {
            var result = await _repairRequestService.UnassignRepairRequestManagerAsync(repairRequestId);
            return this.ToApiResponse(result);
        }

        [HttpGet("finished")]
        public async Task<IActionResult> GetFinishedRepairRequests()
        {
            var finishedRequests = await _repairRequestService.GetFinishedRepairRequestsAsync();
            return Ok(finishedRequests);
        }
    }
}
