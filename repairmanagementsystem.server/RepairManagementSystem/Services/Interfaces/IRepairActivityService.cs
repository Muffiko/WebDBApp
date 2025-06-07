using RepairManagementSystem.Helpers;
using RepairManagementSystem.Models;
using RepairManagementSystem.Models.DTOs;

namespace RepairManagementSystem.Services.Interfaces
{
    public interface IRepairActivityService
    {
        Task<IEnumerable<RepairActivity>> GetAllRepairActivitiesAsync();
        Task<RepairActivity?> GetRepairActivityByIdAsync(int repairActivityId);
        Task<Result> AddRepairActivityAsync(RepairActivityDTO repairActivityDTO);
        Task<Result> UpdateRepairActivityAsync(int repairActivityId, RepairActivityDTO updatedRepairActivityDTO);
        Task<Result> DeleteRepairActivityAsync(int repairActivityId);
    }
}
