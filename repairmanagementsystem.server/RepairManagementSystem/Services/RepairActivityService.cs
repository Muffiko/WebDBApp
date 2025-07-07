using AutoMapper;
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
            repairActivity.Status = "TO DO";
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
            var allowedStatuses = new[] { "TO DO", "IN PROGRESS", "CANCELLED", "COMPLETED" };
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
            if (repairActivity.RepairRequest?.Status == "CANCELLED" || repairActivity.RepairRequest?.Status == "COMPLETED")
            {
                return Result.Fail(400, "Cannot change status of a repair activity for a cancelled or completed repair request.");
            }
            if (newStatus == currentStatus)
            {
                return Result.Fail(400, "The status is already set to the requested value.");
            }

            if (currentStatus == "CANCELLED" || currentStatus == "COMPLETED")
            {
                return Result.Fail(400, "Cannot change status of a repair activity that is already 'CANCELLED' or 'COMPLETED'.");
            }

            if (newStatus == "IN PROGRESS" && currentStatus != "TO DO")
            {
                return Result.Fail(400, "Repair activity must be in 'TO DO' status to change to 'IN PROGRESS'.");
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
                if (repairActivity.WorkerId.HasValue)
                {
                    var worker = await _workerRepository.GetWorkerByIdAsync(repairActivity.WorkerId.Value);
                    if (worker != null)
                    {
                        int inProgressCount = worker.RepairActivities?.Count(a => a != null && a.Status != null && a.Status.ToUpperInvariant() == "IN PROGRESS") ?? 0;
                        if (currentStatus == "IN PROGRESS") inProgressCount--;
                        if (inProgressCount <= 3)
                        {
                            worker.IsAvailable = true;
                            await _workerRepository.UpdateWorkerAsync(worker);
                        }
                    }
                }
            }
            else if (newStatus == "IN PROGRESS")
            {
                if (repairActivity.WorkerId.HasValue)
                {
                    var worker = await _workerRepository.GetWorkerByIdAsync(repairActivity.WorkerId.Value);
                    if (worker != null)
                    {
                        int inProgressCount = worker.RepairActivities?.Count(a => a != null && a.Status != null && a.Status.ToUpperInvariant() == "IN PROGRESS") ?? 0;
                        if (inProgressCount >= 3)
                        {
                            return Result.Fail(400, "Worker already has 3 activities in progress and cannot start another one.");
                        }
                        if (inProgressCount == 2)
                        {
                            worker.IsAvailable = false;
                            await _workerRepository.UpdateWorkerAsync(worker);
                        }
                    }
                }
                repairActivity.StartedAt = DateTime.UtcNow;
                repairActivity.Status = newStatus;
            }
            else if (newStatus == "TO DO" && currentStatus != "TO DO")
            {
                return Result.Fail(400, "Cannot change status of a repair activity to 'TO DO'.");
            }

            var success = await _repairActivityRepository.UpdateRepairActivityAsync(repairActivity);
            return success
                ? Result.Ok("Repair activity status changed successfully.")
                : Result.Fail(500, "Failed to change repair activity status.");
        }
        public async Task<IEnumerable<RepairActivityResponse?>> GetRepairActivitiesByWorkerIdAsync(int workerId)
        {
            var repairActivities = await _repairActivityRepository.GetAllRepairActivitiesAsync();
            if (repairActivities == null || !repairActivities.Any())
            {
                return Enumerable.Empty<RepairActivityResponse?>();
            }
            repairActivities = repairActivities?.Where(ra => ra?.WorkerId == workerId);
            return _mapper.Map<IEnumerable<RepairActivityResponse?>>(repairActivities);
        }

        public async Task<Result> AssignRepairActivityToWorkerAsync(int repairActivityId, RepairActivityAssignRequest request)
        {
            var activity = await _repairActivityRepository.GetRepairActivityByIdAsync(repairActivityId);
            if (activity == null)
            {
                return Result.Fail(404, $"Repair activity with ID {repairActivityId} not found.");
            }

            var worker = await _workerRepository.GetWorkerByIdAsync(request.WorkerId);
            if (worker == null)
            {
                return Result.Fail(404, $"Worker with ID {request.WorkerId} not found.");
            }
            if (!worker.IsAvailable)
            {
                return Result.Fail(400, "Worker is not available.");
            }
            if (activity.WorkerId == request.WorkerId)
            {
                return Result.Fail(400, "This worker is already assigned to the activity.");
            }

            if (activity.WorkerId != null)
            {
                var prevWorker = await _workerRepository.GetWorkerByIdAsync(activity.WorkerId.Value);
                if (prevWorker != null)
                {
                    prevWorker.RepairActivities.Remove(activity);
                    await _workerRepository.UpdateWorkerAsync(prevWorker);
                }
            }


            activity.WorkerId = request.WorkerId;
            worker.RepairActivities ??= [];
            worker.RepairActivities.Add(activity);
            await _workerRepository.UpdateWorkerAsync(worker);
            var success = await _repairActivityRepository.UpdateRepairActivityAsync(activity);
            return success
                ? Result.Ok($"Worker with ID {request.WorkerId} assigned to activity {repairActivityId}.")
                : Result.Fail(500, "Failed to assign worker to activity.");
        }

        public async Task<Result> UnassignRepairActivityWorkerAsync(int repairActivityId)
        {
            var activity = await _repairActivityRepository.GetRepairActivityByIdAsync(repairActivityId);
            if (activity == null)
                return Result.Fail(404, $"Repair activity with ID {repairActivityId} not found.");

            if (activity.WorkerId == null)
                return Result.Fail(400, "No worker is currently assigned to this activity.");

            var worker = await _workerRepository.GetWorkerByIdAsync(activity.WorkerId.Value);
            if (worker != null)
            {
                worker.RepairActivities.Remove(activity);
                await _workerRepository.UpdateWorkerAsync(worker);
            }

            activity.WorkerId = null;
            var success = await _repairActivityRepository.UpdateRepairActivityAsync(activity);
            return success
                ? Result.Ok($"Worker unassigned from activity {repairActivityId}.")
                : Result.Fail(500, "Failed to unassign worker from activity.");
        }
    }
}
