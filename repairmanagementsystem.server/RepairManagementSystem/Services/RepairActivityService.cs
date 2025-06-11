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
            repairActivity.Status = "Open";
            repairActivity.RepairRequestId = repairActivityRequest.RepairRequestId;
            var repairRequest = await _repairRequestRepository.GetRepairRequestByIdAsync(repairActivityRequest.RepairRequestId);
            if (repairRequest == null)
            {
                return Result.Fail(404, "Repair request not found.");
            }
            repairActivity.RepairRequest = repairRequest;
            repairActivity.WorkerId = repairActivityRequest.WorkerId;
            //var worker = await _workerRepository.GetWorkerByIdAsync(repairActivityRequest.WorkerId);
            //if (worker == null)
            //    return Result.Fail(404, "Worker not found.");
            //repairActivity.Worker = worker;
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
    }
}
