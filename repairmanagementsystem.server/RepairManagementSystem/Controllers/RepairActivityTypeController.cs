using Microsoft.AspNetCore.Mvc;
using RepairManagementSystem.Extensions;
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
            return Ok(repairActivityType);
        }

        [HttpPost]
        public async Task<IActionResult> AddRepairActivityType([FromBody] RepairActivityTypeDTO repairActivityTypeDTO)
        {
            var result = await _repairActivityTypeService.AddRepairActivityTypeAsync(repairActivityTypeDTO);
            return this.ToApiResponse(result);
        }

        [HttpPut("{repairActivityTypeId}")]
        public async Task<IActionResult> UpdateRepairActivityType(string repairActivityTypeId, [FromBody] RepairActivityTypeDTO updatedRepairActivityType)
        {
            var result = await _repairActivityTypeService.UpdateRepairActivityTypeAsync(repairActivityTypeId, updatedRepairActivityType);
            return this.ToApiResponse(result);
        }

        [HttpDelete("{repairActivityTypeId}")]
        public async Task<IActionResult> DeleteRepairActivityType(string repairActivityTypeId)
        {
            var result = await _repairActivityTypeService.DeleteRepairActivityTypeAsync(repairActivityTypeId);
            return this.ToApiResponse(result);
        }
    }
}
