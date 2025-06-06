using RepairManagementSystem.Models;
using RepairManagementSystem.Models.DTOs;

namespace RepairManagementSystem.Services.Interfaces
{
    public interface IRepairActivityTypeService
    {
        Task<IEnumerable<RepairActivityType?>?> GetAllRepairActivityTypesAsync();
        Task<RepairActivityType?> GetRepairActivityTypeByIdAsync(string repairActivityTypeId);
        Task<bool> AddRepairActivityTypeAsync(RepairActivityTypeDTO repairActivityType);
        Task<bool> UpdateRepairActivityTypeAsync(string repairActivityTypeId, RepairActivityTypeDTO repairActivityType);
        Task<bool> DeleteRepairActivityTypeAsync(string repairActivityTypeId);
    }
}
