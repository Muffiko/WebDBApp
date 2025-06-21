using RepairManagementSystem.Helpers;
using RepairManagementSystem.Models;
using RepairManagementSystem.Models.DTOs;

namespace RepairManagementSystem.Services.Interfaces
{
    public interface IRepairRequestService
    {
        Task<IEnumerable<RepairRequest?>?> GetAllRepairRequestsAsync();
        Task<RepairRequest?> GetRepairRequestByIdAsync(int repairRequestId);
        Task<Result> AddRepairRequestAsync(RepairRequestAdd request);
        Task<Result> UpdateRepairRequestAsync(int repairRequestId, RepairRequestDTO repairRequest);
        Task<Result> DeleteRepairRequestAsync(int repairRequestId);
        Task<IEnumerable<RepairRequest?>?> GetAllRepairRequestsFromCustomerAsync(int customerId);
        Task<IEnumerable<RepairRequestResponse?>?> GetUnassignedRepairRequestsAsync();
        Task<IEnumerable<RepairRequestResponse?>?> GetActiveRepairRequestsAsync();
        Task<IEnumerable<RepairRequestCustomerResponse?>?> GetAllRepairRequestsForCustomerAsync(int customerId);
        Task<Result> AssignRepairRequestToManagerAsync(int repairRequestId, RepairRequestAssign request);
        Task<Result> ChangeRepairRequestStatusAsync(int repairRequestId, RepairRequestChangeStatusRequest request);
        Task<Result> UnassignRepairRequestManagerAsync(int repairRequestId);
    }
}
