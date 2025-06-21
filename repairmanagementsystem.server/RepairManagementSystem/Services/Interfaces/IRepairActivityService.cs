using RepairManagementSystem.Helpers;
using RepairManagementSystem.Models;
using RepairManagementSystem.Models.DTOs;

namespace RepairManagementSystem.Services.Interfaces
{
    public interface IRepairActivityService
    {
        Task<IEnumerable<RepairActivityResponse?>?> GetAllRepairActivitiesAsync();
        Task<RepairActivityResponse?> GetRepairActivityByIdAsync(int repairActivityId);
        Task<Result> AddRepairActivityAsync(RepairActivityRequest repairActivityRequest);
        Task<Result> UpdateRepairActivityAsync(int repairActivityId, RepairActivityRequest repairActivityRequest);
        Task<Result> DeleteRepairActivityAsync(int repairActivityId);
        Task<Result> PatchRepairActivityAsync(int repairActivityId, RepairActivityPatchRequest patchRequest);
        Task<Result> ChangeRepairActivityStatusAsync(int repairActivityId, ChangeRepairActivityStatusRequest status);
    }
}
