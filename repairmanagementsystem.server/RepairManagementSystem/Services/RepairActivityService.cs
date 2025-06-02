using AutoMapper;
using RepairManagementSystem.Data;
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

        public async Task<RepairActivity?> AddRepairActivityAsync(RepairActivityDTO repairActivityDTO)
        {
            if (repairActivityDTO == null)
                return null;
            var repairActivity = _mapper.Map<RepairActivity>(repairActivityDTO);
            await _repairActivityRepository.AddRepairActivityAsync(repairActivity);
            return repairActivity;
        }

        public async Task<RepairActivity?> UpdateRepairActivityAsync(int repairActivityId, RepairActivityDTO updatedRepairActivityDTO)
        {
            var updatedRepairActivity = await _repairActivityRepository.GetRepairActivityByIdAsync(repairActivityId);
            if (updatedRepairActivity == null)
                return null;
            await _repairActivityRepository.UpdateRepairActivityAsync(_mapper.Map(updatedRepairActivityDTO, updatedRepairActivity));
            return _mapper.Map<RepairActivity>(updatedRepairActivity);
        }

        public async Task<RepairActivity?> DeleteRepairActivityAsync(int repairActivityId)
        {
            var existingRepairActivity = await _repairActivityRepository.GetRepairActivityByIdAsync(repairActivityId);
            if (existingRepairActivity == null)
                return null;
            await _repairActivityRepository.DeleteRepairActivityAsync(repairActivityId);
            return _mapper.Map<RepairActivity>(existingRepairActivity);
        }
    }
}
