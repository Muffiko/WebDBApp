using AutoMapper;
using Azure.Core;
using RepairManagementSystem.Data;
using RepairManagementSystem.Helpers;
using RepairManagementSystem.Models;
using RepairManagementSystem.Models.DTOs;
using RepairManagementSystem.Repositories.Interfaces;
using RepairManagementSystem.Services.Interfaces;

namespace RepairManagementSystem.Services
{
    public class RepairActivityService : IRepairActivityService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IRepairActivityRepository _repairActivityRepository;
        private readonly IRepairActivityTypeRepository _repairActivityTypeRepository;
        private readonly IWorkerRepository _workerRepository;
        private readonly IRepairRequestRepository _repairRequestRepository;

        public RepairActivityService(ApplicationDbContext context, IMapper mapper, IRepairActivityRepository repairActivityRepository, IRepairActivityTypeRepository repairActivityTypeRepository, IWorkerRepository workerRepository, IRepairRequestRepository repairRequestRepository)
        {
            _context = context;
            _mapper = mapper;
            _repairActivityRepository = repairActivityRepository;
            _repairActivityTypeRepository = repairActivityTypeRepository;
            _workerRepository = workerRepository;
            _repairRequestRepository = repairRequestRepository;
        }

        public async Task<IEnumerable<RepairActivityResponse?>?> GetAllRepairActivitiesAsync()
        {
            var repairActivities = await _repairActivityRepository.GetAllRepairActivitiesAsync();
            return _mapper.Map<IEnumerable<RepairActivityResponse?>?>(repairActivities);
        }

        public async Task<RepairActivityResponse?> GetRepairActivityByIdAsync(int repairActivityId)
        {
            var repairActivity = await _repairActivityRepository.GetRepairActivityByIdAsync(repairActivityId);
            return _mapper.Map<RepairActivityResponse?>(repairActivity);
        }

        public async Task<Result> AddRepairActivityAsync(RepairActivityRequest repairActivityRequest)
        {
            var repairActivity = _mapper.Map<RepairActivity>(repairActivityRequest);
            repairActivity.CreatedAt = DateTime.UtcNow;
            repairActivity.Status = "Open";
            repairActivity.RepairRequestId = repairActivityRequest.RepairRequestId;
            var repairRequest = await _repairRequestRepository.GetRepairRequestByIdAsync(repairActivityRequest.RepairRequestId);
            if (repairRequest == null)
            {
                return Result.Fail(404, "Repair request not found.");
            }
            repairActivity.RepairRequest = repairRequest;
            repairActivity.WorkerId = repairActivityRequest.WorkerId;
            repairActivity.RepairActivityTypeId = repairActivityRequest.RepairActivityTypeId;
            var repairActivityType = await _repairActivityTypeRepository.GetRepairActivityTypeByIdAsync(repairActivityRequest.RepairActivityTypeId);
            if (repairActivityType == null)
            {
                return Result.Fail(404, "Repair activity type not found.");
            }
            repairActivity.RepairActivityType = repairActivityType;

            var success = await _repairActivityRepository.AddRepairActivityAsync(repairActivity);
            return success
                ? Result.Ok("Repair activity added successfully.")
                : Result.Fail(500, "Failed to add repair activity.");
        }

        public async Task<Result> UpdateRepairActivityAsync(int repairActivityId, RepairActivityRequest repairActivityRequest)
        {
            var repairActivityType = await _repairActivityTypeRepository.GetRepairActivityTypeByIdAsync(repairActivityRequest.RepairActivityTypeId);
            if (repairActivityType == null)
            {
                return Result.Fail(404, "Repair activity type not found.");
            }
            var updated = _mapper.Map<RepairActivity>(repairActivityRequest);
            updated.RepairActivityId = repairActivityId;
            updated.RepairActivityType = repairActivityType;

            var success = await _repairActivityRepository.UpdateRepairActivityAsync(updated);
            return success
                ? Result.Ok("Repair activity updated successfully.")
                : Result.Fail(500, "Failed to update repair activity.");

        }

        public async Task<Result> DeleteRepairActivityAsync(int repairActivityId)
        {
            if (await _repairActivityRepository.DeleteRepairActivityAsync(repairActivityId))
            {
                return Result.Ok("Repair activity deleted successfully.");
            }
            return Result.Fail(404, "Repair activity not found.");
        }

        public async Task<Result> PatchRepairActivityAsync(int repairActivityId, RepairActivityPatchRequest patchRequest)
        {
            var repairActivity = await _repairActivityRepository.GetRepairActivityByIdAsync(repairActivityId);
            if (repairActivity == null)
            {
                return Result.Fail(404, "Repair activity not found.");
            }

            var updatedFields = new List<string>();

            if (patchRequest.RepairActivityTypeId != null)
            {
                var repairActivityType = await _repairActivityTypeRepository.GetRepairActivityTypeByIdAsync(patchRequest.RepairActivityTypeId);
                if (repairActivityType == null)
                {
                    return Result.Fail(404, "Repair activity type not found.");
                }
                repairActivity.RepairActivityTypeId = patchRequest.RepairActivityTypeId;
                repairActivity.RepairActivityType = repairActivityType;
                updatedFields.Add("RepairActivityTypeId");
            }
            if (patchRequest.Name != null)
            {
                repairActivity.name = patchRequest.Name;
                updatedFields.Add("Name");
            }
            if (patchRequest.SequenceNumber != 0)
            {
                repairActivity.SequenceNumber = patchRequest.SequenceNumber;
                updatedFields.Add("SequenceNumber");
            }
            if (patchRequest.Description != null)
            {
                repairActivity.Description = patchRequest.Description;
                updatedFields.Add("Description");
            }
            if (patchRequest.RepairRequestId != null)
            {
                var repairRequest = await _repairRequestRepository.GetRepairRequestByIdAsync(patchRequest.RepairRequestId.Value);
                if (repairRequest == null)
                {
                    return Result.Fail(404, "Repair request not found.");
                }
                repairActivity.RepairRequestId = patchRequest.RepairRequestId.Value;
                repairActivity.RepairRequest = repairRequest;
                updatedFields.Add("RepairRequestId");
            }
            if (patchRequest.WorkerId != null)
            {
                repairActivity.WorkerId = patchRequest.WorkerId.Value;
                updatedFields.Add("WorkerId");
            }
            if (patchRequest.Result != null)
            {
                repairActivity.Result = patchRequest.Result;
                updatedFields.Add("Result");
            }
            if (patchRequest.Status != null)
            {
                repairActivity.Status = patchRequest.Status;
                updatedFields.Add("Status");
            }
            if (patchRequest.StartedAt != null)
            {
                repairActivity.StartedAt = patchRequest.StartedAt;
                updatedFields.Add("StartedAt");
            }
            if (patchRequest.FinishedAt != null)
            {
                repairActivity.FinishedAt = patchRequest.FinishedAt;
                updatedFields.Add("FinishedAt");
            }

            if (updatedFields.Count == 0)
            {
                return Result.Fail(400, "No fields were provided to update.");
            }

            var success = await _repairActivityRepository.UpdateRepairActivityAsync(repairActivity);
            return success
                ? Result.Ok($"Repair activity patched successfully. Updated fields: {string.Join(", ", updatedFields)}.")
                : Result.Fail(500, "Failed to patch repair activity.");
        }

        public async Task<Result> ChangeRepairActivityStatusAsync(int repairActivityId, ChangeRepairActivityStatusRequest request)
        {
            var allowedStatuses = new[] { "TO_DO", "IN_PROGRESS", "CANCELLED", "COMPLETED" };
            string newStatus = request.Status?.Trim().ToUpperInvariant() ?? string.Empty;

            var repairActivity = await _repairActivityRepository.GetRepairActivityByIdAsync(repairActivityId);
            if (repairActivity == null)
            {
                return Result.Fail(404, "Repair activity not found.");
            }

            string currentStatus = repairActivity.Status?.Trim().ToUpperInvariant() ?? string.Empty;

            if (!allowedStatuses.Contains(newStatus))
            {
                return Result.Fail(400, $"Invalid status provided. Valid statuses are: {string.Join(", ", allowedStatuses)}.");
            }

            if (newStatus == currentStatus)
            {
                return Result.Fail(400, "The status is already set to the requested value.");
            }

            if (currentStatus == "CANCELLED" || currentStatus == "COMPLETED")
            {
                return Result.Fail(400, "Cannot change status of a repair activity that is already 'CANCELLED' or 'COMPLETED'.");
            }

            if (newStatus == "IN_PROGRESS" && currentStatus != "TO_DO")
            {
                return Result.Fail(400, "Repair activity must be in 'TO_DO' status to change to 'IN_PROGRESS'.");
            }

            if ((newStatus == "CANCELLED" || newStatus == "COMPLETED") && string.IsNullOrWhiteSpace(request.Result))
            {
                return Result.Fail(400, "Result must be provided when changing status to 'CANCELLED' or 'COMPLETED'.");
            }

            if (newStatus == "CANCELLED" || newStatus == "COMPLETED")
            {
                repairActivity.FinishedAt = DateTime.UtcNow;
                repairActivity.Result = request.Result!;
                repairActivity.Status = newStatus;
            }
            else if (newStatus == "IN_PROGRESS")
            {
                repairActivity.StartedAt = DateTime.UtcNow;
                repairActivity.Status = newStatus;
            }

            var success = await _repairActivityRepository.UpdateRepairActivityAsync(repairActivity);
            return success
                ? Result.Ok("Repair activity status changed successfully.")
                : Result.Fail(500, "Failed to change repair activity status.");
        }
    }
}
