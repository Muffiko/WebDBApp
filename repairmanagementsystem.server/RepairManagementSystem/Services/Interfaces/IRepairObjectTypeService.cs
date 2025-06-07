using RepairManagementSystem.Helpers;
using RepairManagementSystem.Models;
using RepairManagementSystem.Models.DTOs;

namespace RepairManagementSystem.Services.Interfaces
{
    public interface IRepairObjectTypeService
    {
        Task<IEnumerable<RepairObjectType?>?> GetAllRepairObjectTypesAsync();
        Task<RepairObjectType?> GetRepairObjectTypeByIdAsync(string repairObjectTypeId);
        Task<Result> AddRepairObjectTypeAsync(RepairObjectTypeDTO repairObjectTypeDTO);
        Task<Result> UpdateRepairObjectTypeAsync(string repairObjectTypeId, RepairObjectTypeDTO repairObjectTypeDTO);
        Task<Result> DeleteRepairObjectTypeAsync(string repairObjectTypeId);
        Task<IEnumerable<RepairObjectTypeDTO?>?> GetAllRepairObjectNameAsync();
    }
}
