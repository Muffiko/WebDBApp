using Microsoft.Extensions.Configuration.UserSecrets;
using RepairManagementSystem.Models;
using RepairManagementSystem.Models.DTOs;

namespace RepairManagementSystem.Services.Interfaces
{
    public interface IRepairObjectService
    {
        Task<IEnumerable<RepairObjectResponse?>?> GetAllRepairObjectsAsync();
        Task<RepairObjectResponse?> GetRepairObjectByIdAsync(int repairObjectId);
        Task<bool> AddRepairObjectAsync(int userId, RepairObjectRequest repairObject);
        Task<bool> UpdateRepairObjectAsync(int repairObjectId, RepairObjectRequest repairObject);
        Task<bool> DeleteRepairObjectAsync(int repairObjectId);
        Task<IEnumerable<RepairObject?>?> GetAllRepairObjectsFromCustomerAsync(int customerId);
    }
}
