using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using RepairManagementSystem.Helpers;
using RepairManagementSystem.Models.DTOs;
using RepairManagementSystem.Services.Interfaces;

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
            if (repairRequest == null)
                return ErrorResponseHelper.CreateProblemDetails(
                    HttpContext,
                    "https://tools.ietf.org/html/rfc9110#section-15.5.5",
                    "Repair request not found",
                    404,
                    new { RepairRequest = new[] { $"Repair request with ID {repairRequestId} not found." } }
                );
            return Ok(repairRequest);
        }

        [HttpPost]
        public async Task<IActionResult> AddRepairRequest([FromBody] RepairRequestAdd request)
        {
            int? customerId = User.GetUserId();
            if (customerId == null)
            {
                return Unauthorized();
            }
            var repairObject = await _repairObjectService.GetRepairObjectByIdAsync(request.RepairObjectId);
            if (repairObject == null)
            {
                return BadRequest();
            }
            if (repairObject.CustomerId != customerId.Value)
            {
                return Forbid();
            }
            var existingRequest = await _repairRequestService.GetAllRepairRequestsFromCustomerAsync(customerId.Value);
            if (existingRequest != null && existingRequest.Any(r => r?.RepairObjectId == request.RepairObjectId))
            {
                return ErrorResponseHelper.CreateProblemDetails(
                    HttpContext,
                    "https://tools.ietf.org/html/rfc9110#section-15.5.1",
                    "Repair request already exists",
                    400,
                    new { RepairRequest = new[] { "A pending repair request for this object already exists." } }
                );
            }
            var result = await _repairRequestService.AddRepairRequestAsync(request);
            if (!result)
                return ErrorResponseHelper.CreateProblemDetails(
                    HttpContext,
                    
                    "https://tools.ietf.org/html/rfc9110#section-15.5.1",
                    "Invalid repair request",
                    400,
                    new { RepairRequest = new[] { "Repair request cannot be null or invalid." } }
                );
            return Ok(new { message = "Repair request added successfully." });
        }

        [HttpPut("{repairRequestId:int}")]
        public async Task<IActionResult> UpdateRepairRequest(int repairRequestId, [FromBody] RepairRequestDTO updatedRepairRequest)
        {
            var result = await _repairRequestService.UpdateRepairRequestAsync(repairRequestId, updatedRepairRequest);
            if (!result)
                return ErrorResponseHelper.CreateProblemDetails(
                    HttpContext,
                    "https://tools.ietf.org/html/rfc9110#section-15.5.5",
                    "Repair request not found",
                    404,
                    new { RepairRequest = new[] { $"Repair request with ID {repairRequestId} not found." } }
                );
            return Ok(new { message = $"Repair request with ID {repairRequestId} updated successfully." });
        }
        [HttpDelete("{repairRequestId:int}")]

        public async Task<IActionResult> DeleteRepairRequest(int repairRequestId)
        {
            var result = await _repairRequestService.DeleteRepairRequestAsync(repairRequestId);
            if (!result)
                return ErrorResponseHelper.CreateProblemDetails(
                    HttpContext,
                    "https://tools.ietf.org/html/rfc9110#section-15.5.5",
                    "Repair request not found",
                    404,
                    new { RepairRequest = new[] { $"Repair request with ID {repairRequestId} not found." } }
                );
            return Ok(new { message = $"Repair request with ID {repairRequestId} deleted successfully." });
        }

        [HttpGet("customer/{customerId:int}")]

        public async Task<IActionResult> GetAllRepairRequestsFromCustomer(int customerId)
        {
            var repairRequests = await _repairRequestService.GetAllRepairRequestsFromCustomerAsync(customerId);
            if (repairRequests == null || !repairRequests.Any())
                return ErrorResponseHelper.CreateProblemDetails(
                    HttpContext,
                    "https://tools.ietf.org/html/rfc9110#section-15.5.5",
                    "No repair requests found",
                    404,
                    new { RepairRequest = new[] { $"No repair requests found for customer with ID {customerId}." } }
                );
            return Ok(repairRequests);
        }

        [HttpGet("unassigned")]

        public async Task<IActionResult> GetUnassignedRepairRequests()
        {
            var unassignedRepairRequests = await _repairRequestService.GetUnassignedRepairRequestsAsync();
            if (unassignedRepairRequests == null || !unassignedRepairRequests.Any())
                return ErrorResponseHelper.CreateProblemDetails(
                    HttpContext,
                    "https://tools.ietf.org/html/rfc9110#section-15.5.5",
                    "No unassigned repair requests found",
                    404,
                    new { RepairRequest = new[] { "No unassigned repair requests found." } }
                );
            return Ok(unassignedRepairRequests);
        }

        [HttpGet("active")]

        public async Task<IActionResult> GetActiveRepairRequests()
        {
            var activeRepairRequests = await _repairRequestService.GetActiveRepairRequestsAsync();
            if (activeRepairRequests == null || !activeRepairRequests.Any())
                return ErrorResponseHelper.CreateProblemDetails(
                    HttpContext,
                    "https://tools.ietf.org/html/rfc9110#section-15.5.5",
                    "No active repair requests found",
                    404,
                    new { RepairRequest = new[] { "No active repair requests found." } }
                );
            return Ok(activeRepairRequests);
        }

        [HttpGet("customer/my")]
        public async Task<IActionResult> GetMyRepairRequests()
        {
            var customerId = User.GetUserId();
            if (customerId == null)
            {
                return Unauthorized();
            }
            var repairRequests = await _repairRequestService.GetAllRepairRequestsForCustomerAsync(customerId.Value);
            if (repairRequests == null || !repairRequests.Any())
                return ErrorResponseHelper.CreateProblemDetails(
                    HttpContext,
                    "https://tools.ietf.org/html/rfc9110#section-15.5.5",
                    "No repair requests found",
                    404,
                    new { RepairRequest = new[] { $"No repair requests found." } }
                );
            return Ok(repairRequests);
        }

    }
}