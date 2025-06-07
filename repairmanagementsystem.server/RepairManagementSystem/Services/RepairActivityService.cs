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

        public RepairActivityService(ApplicationDbContext context, IMapper mapper, IRepairActivityRepository repairActivityRepository)
        {
            _context = context;
            _mapper = mapper;
            _repairActivityRepository = repairActivityRepository;
        }

        public async Task<IEnumerable<RepairActivity>> GetAllRepairActivitiesAsync()
        {
            var repairActivities = await _repairActivityRepository.GetAllRepairActivitiesAsync();
            return _mapper.Map<IEnumerable<RepairActivity>>(repairActivities);
        }

        public async Task<RepairActivity?> GetRepairActivityByIdAsync(int repairActivityId)
        {
            var repairActivity = await _repairActivityRepository.GetRepairActivityByIdAsync(repairActivityId);
            return _mapper.Map<RepairActivity>(repairActivity);
        }

        public async Task<Result> AddRepairActivityAsync(RepairActivityDTO repairActivityDTO)
        {
            var repairActivity = _mapper.Map<RepairActivity>(repairActivityDTO);

            if (await _repairActivityRepository.AddRepairActivityAsync(repairActivity))
            {
                return Result.Ok("Repair activity added successfully.");
            }
            return Result.Fail(500,"Failed to add repair activity.");
        }

        public async Task<Result> UpdateRepairActivityAsync(int repairActivityId, RepairActivityDTO updatedRepairActivityDTO)
        {
            var existing = await _repairActivityRepository.GetRepairActivityByIdAsync(repairActivityId);
            if (existing == null)
            {
                return Result.Fail(404, $"Repair activity with ID {repairActivityId} not found.");
            }
            if (await _repairActivityRepository.UpdateRepairActivityAsync(_mapper.Map(updatedRepairActivityDTO, existing)))
            {
                return Result.Ok($"Repair activity with ID {repairActivityId} updated successfully.");
            }
            return Result.Fail(500, $"Failed to update repair activity.");
        }

        public async Task<Result> DeleteRepairActivityAsync(int repairActivityId)
        {
            var existing = await _repairActivityRepository.GetRepairActivityByIdAsync(repairActivityId);
            if (existing == null)
            {
                return Result.Fail(404, $"Repair activity with ID {repairActivityId} not found.");
            }
            if (await _repairActivityRepository.DeleteRepairActivityAsync(repairActivityId))
            {
                return Result.Ok($"Repair activity with ID {repairActivityId} deleted successfully.");
            }
            return Result.Fail(500, $"Failed to delete repair activity.");
        }
    }
}
