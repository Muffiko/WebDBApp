using RepairManagementSystem.Models;

namespace RepairManagementSystem.Repositories.Interfaces
{
    public interface IRepairRequestRepository
    {
        Task<RepairRequest?> GetRepairRequestByIdAsync(int repairRequestId);
        Task<IEnumerable<RepairRequest?>?> GetAllRepairRequestsAsync();
        Task<bool> AddRepairRequestAsync(RepairRequest repairRequest);
        Task<bool> UpdateRepairRequestAsync(RepairRequest repairRequest);
        Task<bool> DeleteRepairRequestAsync(int repairRequestId);
        Task<IEnumerable<RepairRequest?>?> GetAllRepairRequestsFromCustomerAsync(int customerId);
        Task<IEnumerable<RepairRequest?>?> GetUnassignedRepairRequestsAsync();
        Task<IEnumerable<RepairRequest?>?> GetActiveRepairRequestsAsync();
    }
}
