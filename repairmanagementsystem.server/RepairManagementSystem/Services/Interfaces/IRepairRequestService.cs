using RepairManagementSystem.Models;
using RepairManagementSystem.Models.DTOs;

namespace RepairManagementSystem.Services.Interfaces
{
    public interface IRepairRequestService
    {
        Task<IEnumerable<RepairRequest?>?> GetAllRepairRequestsAsync();
        Task<RepairRequest?> GetRepairRequestByIdAsync(int repairRequestId);
        Task<RepairRequest?> AddRepairRequestAsync(RepairRequestDTO repairRequest);
        Task<bool> UpdateRepairRequestAsync(int repairRequestId, RepairRequestDTO repairRequest);
        Task<bool> DeleteRepairRequestAsync(int repairRequestId);
        Task<RepairRequest?> AddRepairRequestAsync(RepairRequestAdd repairRequest);
        Task<IEnumerable<RepairRequest?>?> GetAllRepairRequestsFromCustomerAsync(int customerId);
    }
}
