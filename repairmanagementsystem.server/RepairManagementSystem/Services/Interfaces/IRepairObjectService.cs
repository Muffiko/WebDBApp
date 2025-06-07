using Microsoft.Extensions.Configuration.UserSecrets;
using RepairManagementSystem.Helpers;
using RepairManagementSystem.Models;
using RepairManagementSystem.Models.DTOs;

namespace RepairManagementSystem.Services.Interfaces
{
    public interface IRepairObjectService
    {
        Task<IEnumerable<RepairObjectResponse?>?> GetAllRepairObjectsAsync();
        Task<RepairObjectResponse?> GetRepairObjectByIdAsync(int repairObjectId);
        Task<Result> AddRepairObjectAsync(int userId, RepairObjectRequest repairObject);
        Task<Result> UpdateRepairObjectAsync(int repairObjectId, RepairObjectRequest repairObject);
        Task<Result> DeleteRepairObjectAsync(int repairObjectId);
        Task<IEnumerable<RepairObject?>?> GetAllRepairObjectsFromCustomerAsync(int customerId);
    }
}
