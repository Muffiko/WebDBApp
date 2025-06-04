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
        public RepairRequestController(IRepairRequestService repairRequestService)
        {
            _repairRequestService = repairRequestService;
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
        public async Task<IActionResult> AddRepairRequest([FromBody] RepairRequestDTO repairRequestDTO)
        {
            var result = await _repairRequestService.AddRepairRequestAsync(repairRequestDTO);
            if (result == null)
                return ErrorResponseHelper.CreateProblemDetails(
                    HttpContext,
                    "https://tools.ietf.org/html/rfc9110#section-15.5.1",
                    "Invalid repair request",
                    400,
                    new { RepairRequest = new[] { "Repair request cannot be null or invalid." } }
                );
            return Ok(result);
        }
        [HttpPut("{repairRequestId:int}")]
        public async Task<IActionResult> UpdateRepairRequest(int repairRequestId, [FromBody] RepairRequestDTO updatedRepairRequest)
        {
            var result = await _repairRequestService.UpdateRepairRequestAsync(repairRequestId, updatedRepairRequest);
            if (result == null)
                return ErrorResponseHelper.CreateProblemDetails(
                    HttpContext,
                    "https://tools.ietf.org/html/rfc9110#section-15.5.5",
                    "Repair request not found",
                    404,
                    new { RepairRequest = new[] { $"Repair request with ID {repairRequestId} not found." } }
                );
            return Ok($"Repair request with ID {repairRequestId} updated successfully.");
        }
        [HttpDelete("{repairRequestId:int}")]
        public async Task<IActionResult> DeleteRepairRequest(int repairRequestId)
        {
            var result = await _repairRequestService.DeleteRepairRequestAsync(repairRequestId);
            if (result == null)
                return ErrorResponseHelper.CreateProblemDetails(
                    HttpContext,
                    "https://tools.ietf.org/html/rfc9110#section-15.5.5",
                    "Repair request not found",
                    404,
                    new { RepairRequest = new[] { $"Repair request with ID {repairRequestId} not found." } }
                );
            return Ok($"Repair request with ID {repairRequestId} deleted successfully.");
        }
    }
}
