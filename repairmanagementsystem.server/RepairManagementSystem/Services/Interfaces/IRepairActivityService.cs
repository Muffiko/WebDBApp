using RepairManagementSystem.Models;
using RepairManagementSystem.Models.DTOs;

namespace RepairManagementSystem.Services.Interfaces
{
    public interface IRepairActivityService
    {
        Task<IEnumerable<RepairActivity>> GetAllRepairActivitiesAsync();
        Task<RepairActivity?> GetRepairActivityByIdAsync(int repairActivityId);
        Task<RepairActivity?> AddRepairActivityAsync(RepairActivityDTO repairActivityDTO);
        Task<RepairActivity?> UpdateRepairActivityAsync(int repairActivityId, RepairActivityDTO updatedRepairActivityDTO);
        Task<RepairActivity?> DeleteRepairActivityAsync(int repairActivityId);
    }
}
