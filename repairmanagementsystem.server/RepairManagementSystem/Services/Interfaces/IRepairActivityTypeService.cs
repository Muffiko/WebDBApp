using RepairManagementSystem.Helpers;
using RepairManagementSystem.Models;
using RepairManagementSystem.Models.DTOs;

namespace RepairManagementSystem.Services.Interfaces
{
    public interface IRepairActivityTypeService
    {
        Task<IEnumerable<RepairActivityType?>?> GetAllRepairActivityTypesAsync();
        Task<RepairActivityType?> GetRepairActivityTypeByIdAsync(string repairActivityTypeId);
        Task<Result> AddRepairActivityTypeAsync(RepairActivityTypeDTO repairActivityType);
        Task<Result> UpdateRepairActivityTypeAsync(string repairActivityTypeId, RepairActivityTypeDTO repairActivityType);
        Task<Result> DeleteRepairActivityTypeAsync(string repairActivityTypeId);
    }
}
