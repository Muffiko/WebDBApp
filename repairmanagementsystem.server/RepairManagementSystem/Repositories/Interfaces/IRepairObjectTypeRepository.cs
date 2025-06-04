using RepairManagementSystem.Models;

namespace RepairManagementSystem.Repositories.Interfaces
{
    public interface IRepairObjectTypeRepository
    {
        Task<RepairObjectType?> GetRepairObjectTypeByIdAsync(string repairObjectTypeId);
        Task<IEnumerable<RepairObjectType?>?> GetAllRepairObjectTypesAsync();
        Task<bool> AddRepairObjectTypeAsync(RepairObjectType repairObjectType);
        Task<bool> UpdateRepairObjectTypeAsync(RepairObjectType repairObjectType);
        Task<bool> DeleteRepairObjectTypeAsync(string repairObjectTypeId);
    }
}
