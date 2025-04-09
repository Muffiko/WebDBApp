using RepairManagementSystem.Models;

namespace RepairManagementSystem.Repositories.Interfaces
{
    public interface IRepairActivityTypeRepository
    {
        Task<RepairActivityType> GetRepairActivityTypeByIdAsync(int repairActivityTypeId);
        Task<IEnumerable<RepairActivityType>> GetAllRepairActivityTypesAsync();
        Task AddRepairActivityTypeAsync(RepairActivityType repairActivityType);
        Task UpdateRepairActivityTypeAsync(RepairActivityType repairActivityType);
        Task DeleteRepairActivityTypeAsync(int repairActivityTypeId);
    }
}
