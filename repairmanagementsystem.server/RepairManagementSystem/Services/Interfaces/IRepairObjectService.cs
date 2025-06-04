using RepairManagementSystem.Models;
using RepairManagementSystem.Models.DTOs;

namespace RepairManagementSystem.Services.Interfaces
{
    public interface IRepairObjectService
    {
        Task<IEnumerable<RepairObject>> GetAllRepairObjectsAsync();
        Task<RepairObject?> GetRepairObjectByIdAsync(int repairObjectId);
        Task<RepairObject?> AddRepairObjectAsync(RepairObjectDTO repairObject);
        Task<RepairObject?> UpdateRepairObjectAsync(int repairObjectId, RepairObjectDTO repairObject);
        Task<RepairObject?> DeleteRepairObjectAsync(int repairObjectId);
        Task<RepairObject?> AddRepairObjectAsync(RepairObjectAddDTO repairObject);
        Task<IEnumerable<RepairObject>> GetAllRepairObjectsFromCustomerAsync(int customerId);
    }
}
