using RepairManagementSystem.Models;
using RepairManagementSystem.Models.DTOs;

namespace RepairManagementSystem.Services.Interfaces
{
    public interface IRepairObjectTypeService
    {
        Task<IEnumerable<RepairObjectType?>?> GetAllRepairObjectTypesAsync();
        Task<RepairObjectType?> GetRepairObjectTypeByIdAsync(string repairObjectTypeId);
        Task<bool> AddRepairObjectTypeAsync(RepairObjectTypeDTO repairObjectType);
        Task<bool> UpdateRepairObjectTypeAsync(string repairObjectTypeId, RepairObjectTypeDTO repairObjectType);
        Task<bool> DeleteRepairObjectTypeAsync(string repairObjectTypeId);
        Task<IEnumerable<RepairObjectTypeDTO?>?> GetAllRepairObjectNameAsync();
    }
}
