using Microsoft.AspNetCore.Mvc;
using RepairManagementSystem.Models.DTOs;
using RepairManagementSystem.Services.Interfaces;
using RepairManagementSystem.Helpers;
using Microsoft.AspNetCore.Authorization;
using RepairManagementSystem.Extensions;

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
            return Ok(repairObjectType);
        }

        [HttpPost]
        public async Task<IActionResult> AddRepairObjectType([FromBody] RepairObjectTypeDTO repairObjectTypeDTO)
        {
            var result = await _repairObjectTypeService.AddRepairObjectTypeAsync(repairObjectTypeDTO);
            return this.ToApiResponse(result);
        }

        [HttpPut("{repairObjectTypeId}")]
        public async Task<IActionResult> UpdateRepairObjectType(string repairObjectTypeId, [FromBody] RepairObjectTypeDTO updatedRepairObjectType)
        {
            var result = await _repairObjectTypeService.UpdateRepairObjectTypeAsync(repairObjectTypeId, updatedRepairObjectType);
            return this.ToApiResponse(result);
        }

        [HttpDelete("{repairObjectTypeId}")]
        public async Task<IActionResult> DeleteRepairObjectType(string repairObjectTypeId)
        {
            var result = await _repairObjectTypeService.DeleteRepairObjectTypeAsync(repairObjectTypeId);
            return this.ToApiResponse(result);
        }
    }
}
