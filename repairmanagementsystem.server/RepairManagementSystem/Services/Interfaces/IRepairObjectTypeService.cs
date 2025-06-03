using RepairManagementSystem.Models;
using RepairManagementSystem.Models.DTOs;

namespace RepairManagementSystem.Services.Interfaces
{
    public interface IRepairObjectTypeService
    {
        Task<IEnumerable<RepairObjectType>> GetAllRepairObjectTypesAsync();
        Task<RepairObjectType?> GetRepairObjectTypeByIdAsync(string repairObjectTypeId);
        Task<RepairObjectType?> AddRepairObjectTypeAsync(RepairObjectTypeDTO repairObjectType);
        Task<RepairObjectType?> UpdateRepairObjectTypeAsync(string repairObjectTypeId, RepairObjectTypeDTO repairObjectType);
        Task<RepairObjectType?> DeleteRepairObjectTypeAsync(string repairObjectTypeId);
        Task<IEnumerable<RepairObjectTypeDTO>> GetAllRepairObjectNameAsync();
    }
}
