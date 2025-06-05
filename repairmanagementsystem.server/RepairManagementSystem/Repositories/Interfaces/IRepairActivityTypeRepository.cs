using RepairManagementSystem.Models;

namespace RepairManagementSystem.Repositories.Interfaces
{
    public interface IRepairActivityTypeRepository
    {
        Task<RepairActivityType?> GetRepairActivityTypeByIdAsync(string repairActivityTypeId);
        Task<IEnumerable<RepairActivityType?>?> GetAllRepairActivityTypesAsync();
        Task<bool> AddRepairActivityTypeAsync(RepairActivityType repairActivityType);
        Task<bool> UpdateRepairActivityTypeAsync(RepairActivityType repairActivityType);
        Task<bool> DeleteRepairActivityTypeAsync(string repairActivityTypeId);
    }
}
