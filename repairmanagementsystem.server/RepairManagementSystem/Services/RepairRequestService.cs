using AutoMapper;
using RepairManagementSystem.Data;
using RepairManagementSystem.Helpers;
using RepairManagementSystem.Models;
using RepairManagementSystem.Models.DTOs;
using RepairManagementSystem.Repositories;
using RepairManagementSystem.Repositories.Interfaces;
using RepairManagementSystem.Services.Interfaces;

namespace RepairManagementSystem.Services
{
    public class RepairRequestService : IRepairRequestService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IRepairRequestRepository _repairRequestRepository;
        private readonly IRepairObjectRepository _repairObjectRepository;
        private readonly IManagerRepository _managerRepository;

        public RepairRequestService(ApplicationDbContext context, IMapper mapper, IRepairRequestRepository repairRequestRepository, IRepairObjectRepository repairObjectRepository, IManagerRepository managerRepository)
        {
            _context = context;
            _mapper = mapper;
            _repairRequestRepository = repairRequestRepository;
            _repairObjectRepository = repairObjectRepository;
            _managerRepository = managerRepository;
        }

        public async Task<IEnumerable<RepairRequest?>?> GetAllRepairRequestsAsync()
        {
            var repairRequests = await _repairRequestRepository.GetAllRepairRequestsAsync();
            return _mapper.Map<IEnumerable<RepairRequest?>?>(repairRequests);
        }

        public async Task<RepairRequest?> GetRepairRequestByIdAsync(int repairRequestId)
        {
            var repairRequest = await _repairRequestRepository.GetRepairRequestByIdAsync(repairRequestId);
            return _mapper.Map<RepairRequest?>(repairRequest);
        }

        public async Task<Result> AddRepairRequestAsync(RepairRequestAdd request)
        {
            var repairRequest = _mapper.Map<RepairRequest>(request);
            repairRequest.CreatedAt = DateTime.UtcNow;
            repairRequest.Status = !string.IsNullOrWhiteSpace(request.Status) ? request.Status : "OPEN";
            repairRequest.IsPaid = false;
            repairRequest.RepairObjectId = request.RepairObjectId;
            var repairObject = await _repairObjectRepository.GetRepairObjectByIdAsync(request.RepairObjectId);
            if (repairObject == null)
            {
                return Result.Fail(404, "Repair object not found.");
            }
            repairRequest.RepairObject = repairObject;
            await _repairRequestRepository.AddRepairRequestAsync(repairRequest);
            return Result.Ok("Repair request added successfully.");
        }

        public async Task<Result> UpdateRepairRequestAsync(int repairRequestId, RepairRequestDTO repairRequestDTO)
        {
            var updated = _mapper.Map<RepairRequest>(repairRequestDTO);
            updated.RepairRequestId = repairRequestId;
            var success = await _repairRequestRepository.UpdateRepairRequestAsync(updated);
            if (!success)
            {
                return Result.Fail(404, $"Repair request with ID {repairRequestId} not found.");
            }
            return Result.Ok($"Repair request with ID {repairRequestId} updated successfully.");
        }

        public async Task<Result> DeleteRepairRequestAsync(int repairRequestId)
        {
            var success = await _repairRequestRepository.DeleteRepairRequestAsync(repairRequestId);
            if (!success)
            {
                return Result.Fail(404, $"Repair request with ID {repairRequestId} not found.");
            }
            return Result.Ok($"Repair request with ID {repairRequestId} deleted successfully.");
        }

        public async Task<IEnumerable<RepairRequest?>?> GetAllRepairRequestsFromCustomerAsync(int customerId)
        {
            return await _repairRequestRepository.GetAllRepairRequestsFromCustomerAsync(customerId);
        }

        public async Task<IEnumerable<RepairRequestResponse?>?> GetUnassignedRepairRequestsAsync()
        {
            var repairRequest = await _repairRequestRepository.GetUnassignedRepairRequestsAsync();
            return _mapper.Map<IEnumerable<RepairRequestResponse?>?>(repairRequest);
        }

        public async Task<IEnumerable<RepairRequestResponse?>?> GetActiveRepairRequestsAsync()
        {
            var repairRequest = await _repairRequestRepository.GetActiveRepairRequestsAsync();
            return _mapper.Map<IEnumerable<RepairRequestResponse?>?>(repairRequest);
        }

        public async Task<IEnumerable<RepairRequestCustomerResponse?>?> GetAllRepairRequestsForCustomerAsync(int customerId)
        {
            var repairRequests = await _repairRequestRepository.GetAllRepairRequestsFromCustomerAsync(customerId);
            return _mapper.Map<IEnumerable<RepairRequestCustomerResponse?>?>(repairRequests);
        }

        public async Task<Result> AssignRepairRequestToManagerAsync(int repairRequestId, RepairRequestAssign request)
        {
            var repairRequest = await _repairRequestRepository.GetRepairRequestByIdAsync(repairRequestId);
            var manager = await _managerRepository.GetManagerByIdAsync(request.ManagerId);
            if (manager == null)
            {
                return Result.Fail(404, $"Manager with ID {request.ManagerId} not found.");
            }

            if (repairRequest == null)
            {
                return Result.Fail(404, $"Repair request with ID {repairRequestId} not found.");
            }

            if (repairRequest.ManagerId.HasValue)
            {
                return Result.Fail(400, "Repair request is already assigned to a manager.");
            }

            repairRequest.ManagerId = request.ManagerId;

            manager.ActiveRepairsCount++;
            manager.RepairRequests.Add(repairRequest);
            if (await _managerRepository.UpdateManagerAsync(manager))
            {
                return await _repairRequestRepository.UpdateRepairRequestAsync(repairRequest)
                    ? Result.Ok($"Repair request with ID {repairRequestId} assigned to manager with ID {request.ManagerId} successfully.")
                    : Result.Fail(500, "Failed to assign repair request to manager.");
            }

            return Result.Fail(500, "Failed to update manager.");
        }

        public async Task<Result> ChangeRepairRequestStatusAsync(int repairRequestId, RepairRequestChangeStatusRequest request)
        {
            var allowedStatuses = new[] { "NEW", "OPEN", "IN_PROGRESS", "CANCELLED", "COMPLETED" };
            string newStatus = request.NewStatus?.Trim().ToUpperInvariant() ?? string.Empty;

            var repairRequest = await _repairRequestRepository.GetRepairRequestByIdAsync(repairRequestId);
            if (repairRequest == null)
            {
                return Result.Fail(404, "Repair request not found.");
            }

            string currentStatus = repairRequest.Status?.Trim().ToUpperInvariant() ?? string.Empty;

            if (!allowedStatuses.Contains(newStatus))
            {
                return Result.Fail(400, $"Invalid status provided. Valid statuses are: {string.Join(", ", allowedStatuses)}.");
            }

            if (newStatus == currentStatus)
            {
                return Result.Fail(400, "The status is already set to the requested value.");
            }

            int currentIndex = Array.IndexOf(allowedStatuses, currentStatus);
            int newIndex = Array.IndexOf(allowedStatuses, newStatus);
            if (newIndex < currentIndex)
            {
                return Result.Fail(400, "Cannot revert to a previous status.");
            }

            if (currentStatus == "NEW" && newStatus != "OPEN")
            {
                return Result.Fail(400, "Can only change status from 'NEW' to 'OPEN'.");
            }

            if (newStatus == "CANCELLED" || newStatus == "COMPLETED")
            {
                repairRequest.FinishedAt = DateTime.UtcNow;
                repairRequest.Result = request.Result!;
                repairRequest.Status = newStatus;
            }
            else if (newStatus == "IN_PROGRESS")
            {
                repairRequest.StartedAt = DateTime.UtcNow;
                repairRequest.Status = newStatus;
            }
            else if (newStatus == "OPEN")
            {
                repairRequest.Status = newStatus;
            }

            var updateSuccess = await _repairRequestRepository.UpdateRepairRequestAsync(repairRequest);
            if (!updateSuccess)
            {
                return Result.Fail(500, "Failed to update repair request status.");
            }

            return Result.Ok($"Repair request status updated to '{newStatus}'.");
        }

        public async Task<Result> UnassignRepairRequestManagerAsync(int repairRequestId)
        {
            var repairRequest = await _repairRequestRepository.GetRepairRequestByIdAsync(repairRequestId);
            if (repairRequest == null)
            {
                return Result.Fail(404, $"Repair request with ID {repairRequestId} not found.");
            }

            if (!repairRequest.ManagerId.HasValue)
            {
                return Result.Fail(400, "Repair request is not assigned to any manager.");
            }

            var manager = await _managerRepository.GetManagerByIdAsync(repairRequest.ManagerId.Value);
            if (manager != null)
            {
                manager.ActiveRepairsCount = Math.Max(0, manager.ActiveRepairsCount - 1);
                manager.RepairRequests.Remove(repairRequest);
                await _managerRepository.UpdateManagerAsync(manager);
            }

            repairRequest.ManagerId = null;
            var updateSuccess = await _repairRequestRepository.UpdateRepairRequestAsync(repairRequest);
            if (!updateSuccess)
            {
                return Result.Fail(500, "Failed to unassign manager from repair request.");
            }

            return Result.Ok($"Manager unassigned from repair request with ID {repairRequestId}.");
        }
    }
}
