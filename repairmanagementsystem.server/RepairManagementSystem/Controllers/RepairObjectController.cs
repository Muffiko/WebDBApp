using Microsoft.AspNetCore.Authorization;
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
    public class RepairObjectController : ControllerBase
    {
        private readonly IRepairObjectService _repairObjectService;
        public RepairObjectController(IRepairObjectService repairObjectService)
        {
            _repairObjectService = repairObjectService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRepairObjects()
        {
            var repairObjects = await _repairObjectService.GetAllRepairObjectsAsync();
            return Ok(repairObjects);
        }

        [HttpGet("{repairObjectId:int}")]
        public async Task<IActionResult> GetRepairObject(int repairObjectId)
        {
            var repairObject = await _repairObjectService.GetRepairObjectByIdAsync(repairObjectId);
            return Ok(repairObject);
        }

        [HttpPost]
        public async Task<IActionResult> AddRepairObject([FromBody] RepairObjectRequest repairObjectRequest)
        {
            int? userId = User.GetUserId();
            if (userId == null)
            {
                return this.ToApiResponse(Result.Fail(401, "User is not authenticated."));
            }
            var result = await _repairObjectService.AddRepairObjectAsync(userId.Value, repairObjectRequest);
            return this.ToApiResponse(result);
        }

        [HttpPut("{repairObjectId:int}")]
        public async Task<IActionResult> UpdateRepairObject(int repairObjectId, [FromBody] RepairObjectRequest updatedRepairObject)
        {
            int? userId = User.GetUserId();
            if (userId == null)
            {
                return this.ToApiResponse(Result.Fail(401, "User is not authenticated."));
            }
            var result = await _repairObjectService.UpdateRepairObjectAsync(repairObjectId, updatedRepairObject);
            return this.ToApiResponse(result);
        }

        [HttpDelete("{repairObjectId:int}")]
        public async Task<IActionResult> DeleteRepairObject(int repairObjectId)
        {
            var result = await _repairObjectService.DeleteRepairObjectAsync(repairObjectId);
            return this.ToApiResponse(result);
        }

        [HttpGet("customer/{customerId:int}")]
        public async Task<IActionResult> GetAllRepairObjectsFromCustomer(int customerId)
        {
            var repairObjects = await _repairObjectService.GetAllRepairObjectsFromCustomerAsync(customerId);
            return Ok(repairObjects);
        }

        [HttpGet("customer/my")]
        public async Task<IActionResult> GetAllRepairObjectsForCurrentUser()
        {
            int? userId = User.GetUserId();
            if (userId == null)
            {
                return this.ToApiResponse(Result.Fail(401, "User is not authenticated."));
            }

            var repairObjects = await _repairObjectService.GetAllRepairObjectsFromCustomerAsync(userId.Value);
            return Ok(repairObjects);
        }
    }
}
