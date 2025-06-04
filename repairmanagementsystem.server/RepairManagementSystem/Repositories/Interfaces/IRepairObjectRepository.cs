using RepairManagementSystem.Models;

namespace RepairManagementSystem.Repositories.Interfaces
{
    public interface IRepairObjectRepository
    {
        Task<RepairObject> GetRepairObjectByIdAsync(int repairObjectId);
        Task<IEnumerable<RepairObject>> GetAllRepairObjectsAsync();
        Task AddRepairObjectAsync(RepairObject repairObject);
        Task UpdateRepairObjectAsync(RepairObject repairObject);
        Task DeleteRepairObjectAsync(int repairObjectId);
        Task<IEnumerable<RepairObject>> GetAllRepairObjectsFromCustomerAsync(int customerId);
    }
}
