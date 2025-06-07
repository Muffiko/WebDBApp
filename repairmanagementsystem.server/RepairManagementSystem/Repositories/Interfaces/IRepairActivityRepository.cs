using RepairManagementSystem.Models;

namespace RepairManagementSystem.Repositories.Interfaces
{
    public interface IRepairActivityRepository
    {
        Task<RepairActivity?> GetRepairActivityByIdAsync(int repairActivityId);
        Task<IEnumerable<RepairActivity?>?> GetAllRepairActivitiesAsync();
        Task<bool> AddRepairActivityAsync(RepairActivity repairActivity);
        Task<bool> UpdateRepairActivityAsync(RepairActivity repairActivity);
        Task<bool> DeleteRepairActivityAsync(int repairActivityId);
    }
}
