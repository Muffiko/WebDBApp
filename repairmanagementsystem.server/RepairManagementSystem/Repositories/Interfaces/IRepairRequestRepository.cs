using RepairManagementSystem.Models;

namespace RepairManagementSystem.Repositories.Interfaces
{
    public interface IRepairRequestRepository
    {
        Task<RepairRequest> GetRepairRequestByIdAsync(int repairRequestId);
        Task<IEnumerable<RepairRequest>> GetAllRepairRequestsAsync();
        Task AddRepairRequestAsync(RepairRequest repairRequest);
        Task UpdateRepairRequestAsync(RepairRequest repairRequest);
        Task DeleteRepairRequestAsync(int repairRequestId);
    }
}
