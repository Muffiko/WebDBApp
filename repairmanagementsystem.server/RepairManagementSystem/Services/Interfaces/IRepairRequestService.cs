using RepairManagementSystem.Models;
using RepairManagementSystem.Models.DTOs;

namespace RepairManagementSystem.Services.Interfaces
{
    public interface IRepairRequestService
    {
        Task<IEnumerable<RepairRequest?>?> GetAllRepairRequestsAsync();
        Task<RepairRequest?> GetRepairRequestByIdAsync(int repairRequestId);
        Task<bool> AddRepairRequestAsync(RepairRequestAdd request);
        Task<bool> UpdateRepairRequestAsync(int repairRequestId, RepairRequestDTO repairRequest);
        Task<bool> DeleteRepairRequestAsync(int repairRequestId);
        Task<IEnumerable<RepairRequest?>?> GetAllRepairRequestsFromCustomerAsync(int customerId);
        Task<IEnumerable<RepairRequestResponse?>?> GetUnassignedRepairRequestsAsync();
        Task<IEnumerable<RepairRequestResponse?>?> GetActiveRepairRequestsAsync();
        Task<IEnumerable<RepairRequestCustomerResponse?>?> GetAllRepairRequestsForCustomerAsync(int customerId);
    }
}
