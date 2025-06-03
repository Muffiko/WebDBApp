using RepairManagementSystem.Models;

namespace RepairManagementSystem.Repositories.Interfaces
{
    public interface IRepairObjectTypeRepository
    {
        Task<RepairObjectType> GetRepairObjectTypeByIdAsync(string repairObjectTypeId);
        Task<IEnumerable<RepairObjectType>> GetAllRepairObjectTypesAsync();
        Task AddRepairObjectTypeAsync(RepairObjectType repairObjectType);
        Task UpdateRepairObjectTypeAsync(RepairObjectType repairObjectType);
        Task DeleteRepairObjectTypeAsync(string repairObjectTypeId);
    }
}
