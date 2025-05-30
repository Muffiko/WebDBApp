using RepairManagementSystem.Models;

namespace RepairManagementSystem.Repositories.Interfaces
{
    public interface IRepairActivityRepository
    {
        Task<RepairActivity> GetRepairActivityByIdAsync(int repairActivityId);
        Task<IEnumerable<RepairActivity>> GetAllRepairActivitiesAsync();
        Task AddRepairActivityAsync(RepairActivity repairActivity);
        Task UpdateRepairActivityAsync(RepairActivity repairActivity);
        Task DeleteRepairActivityAsync(int repairActivityId);
    }
}
