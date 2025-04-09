using RepairManagementSystem.Models;

namespace RepairManagementSystem.Repositories.Interfaces
{
    public interface IRepairObjectTypeRepository
    {
        Task<RepairObjectType> GetRepairObjectTypeByIdAsync(int repairObjectTypeId);
        Task<IEnumerable<RepairObjectType>> GetAllRepairObjectTypesAsync();
        Task AddRepairObjectTypeAsync(RepairObjectType repairObjectType);
        Task UpdateRepairObjectTypeAsync(RepairObjectType repairObjectType);
        Task DeleteRepairObjectTypeAsync(int repairObjectTypeId);
    }
}
