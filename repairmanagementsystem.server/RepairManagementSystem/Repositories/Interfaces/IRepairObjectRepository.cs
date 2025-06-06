using RepairManagementSystem.Models;

namespace RepairManagementSystem.Repositories.Interfaces
{
    public interface IRepairObjectRepository
    {
        Task<RepairObject?> GetRepairObjectByIdAsync(int repairObjectId);
        Task<IEnumerable<RepairObject?>> GetAllRepairObjectsAsync();
        Task<bool> AddRepairObjectAsync(RepairObject repairObject);
        Task<bool> UpdateRepairObjectAsync(RepairObject repairObject);
        Task<bool> DeleteRepairObjectAsync(int repairObjectId);
        Task<IEnumerable<RepairObject?>> GetAllRepairObjectsFromCustomerAsync(int customerId);
    }
}
