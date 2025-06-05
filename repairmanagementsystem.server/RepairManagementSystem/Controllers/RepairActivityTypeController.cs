using Microsoft.AspNetCore.Mvc;
using RepairManagementSystem.Helpers;
using RepairManagementSystem.Models;
using RepairManagementSystem.Models.DTOs;
using RepairManagementSystem.Services.Interfaces;

namespace RepairManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class RepairActivityTypeController : ControllerBase
    {
        private readonly IRepairActivityTypeService _repairActivityTypeService;
        public RepairActivityTypeController(IRepairActivityTypeService repairActivityTypeService)
        {
            _repairActivityTypeService = repairActivityTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRepairActivityTypes()
        {
            var repairActivityTypes = await _repairActivityTypeService.GetAllRepairActivityTypesAsync();
            return Ok(repairActivityTypes);
        }

        [HttpGet("{repairActivityTypeId}")]
        public async Task<IActionResult> GetRepairActivityType(string repairActivityTypeId)
        {
            var repairActivityType = await _repairActivityTypeService.GetRepairActivityTypeByIdAsync(repairActivityTypeId);
            if (repairActivityType == null)
                return ErrorResponseHelper.CreateProblemDetails(
                    HttpContext,
                    "https://tools.ietf.org/html/rfc9110#section-15.5.5",
                    "Repair activity type not found",
                    404,
                    new { RepairActivityType = new[] { $"Repair object type with ID {repairActivityTypeId} not found." } }
                );
            return Ok(repairActivityType);
        }

        [HttpPost]
        public async Task<IActionResult> AddRepairActivityType([FromBody] RepairActivityTypeDTO repairActivityTypeDTO)
        {
            var result = await _repairActivityTypeService.AddRepairActivityTypeAsync(repairActivityTypeDTO);
            if (!result)
                return ErrorResponseHelper.CreateProblemDetails(
                    HttpContext,
                    "https://tools.ietf.org/html/rfc9110#section-15.5.1",
                    "Invalid repair activity type",
                    400,
                    new { RepairActivityType = new[] { "Repair activity type cannot be null or invalid." } }
                );
            return Ok(new { message = "Repair activity type added successfully." });
        }

        [HttpPut("{repairActivityTypeId}")]
        public async Task<IActionResult> UpdateRepairActivityType(string repairActivityTypeId, [FromBody] RepairActivityTypeDTO updatedRepairActivityType)
        {
            var result = await _repairActivityTypeService.UpdateRepairActivityTypeAsync(repairActivityTypeId, updatedRepairActivityType);
            if (!result)
                return ErrorResponseHelper.CreateProblemDetails(
                    HttpContext,
                    "https://tools.ietf.org/html/rfc9110#section-15.5.5",
                    "Repair activity type not found",
                    404,
                    new { RepairActivityType = new[] { $"Repair activity type with ID {repairActivityTypeId} not found." } }
                );
            return Ok(new { message = $"Repair activity type with ID {repairActivityTypeId} updated successfully." });
        }

        [HttpDelete("{repairActivityTypeId}")]
        public async Task<IActionResult> DeleteRepairActivityType(string repairActivityTypeId)
        {
            var result = await _repairActivityTypeService.DeleteRepairActivityTypeAsync(repairActivityTypeId);
            if (!result)
                return ErrorResponseHelper.CreateProblemDetails(
                    HttpContext,
                    "https://tools.ietf.org/html/rfc9110#section-15.5.5",
                    "Repair activity type not found",
                    404,
                    new { RepairActivityType = new[] { $"Repair activity type with ID {repairActivityTypeId} not found." } }
                );
            return Ok(new { message = $"Repair activity type with ID {repairActivityTypeId} deleted successfully." });
        }
    }
}
