using AutoMapper;
using RepairManagementSystem.Data;
using RepairManagementSystem.Helpers;
using RepairManagementSystem.Models;
using RepairManagementSystem.Models.DTOs;
using RepairManagementSystem.Repositories.Interfaces;
using RepairManagementSystem.Services.Interfaces;

namespace RepairManagementSystem.Services
{
    public class RepairActivityTypeService : IRepairActivityTypeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IRepairActivityTypeRepository _repairActivityTypeRepository;

        public RepairActivityTypeService(ApplicationDbContext context, IMapper mapper, IRepairActivityTypeRepository repairActivityTypeRepository)
        {
            _context = context;
            _mapper = mapper;
            _repairActivityTypeRepository = repairActivityTypeRepository;
        }

        public async Task<IEnumerable<RepairActivityType?>?> GetAllRepairActivityTypesAsync()
        {
            var repairActivityTypes = await _repairActivityTypeRepository.GetAllRepairActivityTypesAsync();
            return _mapper.Map<IEnumerable<RepairActivityType?>?>(repairActivityTypes);
        }

        public async Task<RepairActivityType?> GetRepairActivityTypeByIdAsync(string repairActivityTypeId)
        {
            var repairActivityType = await _repairActivityTypeRepository.GetRepairActivityTypeByIdAsync(repairActivityTypeId);
            return _mapper.Map<RepairActivityType?>(repairActivityType);
        }

        public async Task<Result> AddRepairActivityTypeAsync(RepairActivityTypeDTO repairActivityTypeDTO)
        {
            if (repairActivityTypeDTO == null)
                return Result.Fail(400, "Invalid repair activity type data.");
            var repairActivityType = _mapper.Map<RepairActivityType>(repairActivityTypeDTO);
            var success = await _repairActivityTypeRepository.AddRepairActivityTypeAsync(repairActivityType);
            return success
                ? Result.Ok("Repair activity type added successfully.")
                : Result.Fail(500, "Failed to add repair activity type.");
        }

        public async Task<Result> UpdateRepairActivityTypeAsync(string repairActivityTypeId, RepairActivityTypeDTO repairActivityTypeDTO)
        {
            if (repairActivityTypeId == repairActivityTypeDTO.RepairActivityTypeId)
            {
                var updated = _mapper.Map<RepairActivityType?>(repairActivityTypeDTO);
                if (updated == null)
                    return Result.Fail(400, "Invalid repair activity type data.");
                updated.RepairActivityTypeId = repairActivityTypeId;
                var success = await _repairActivityTypeRepository.UpdateRepairActivityTypeAsync(updated);
                return success
                    ? Result.Ok("Repair activity type updated successfully.")
                    : Result.Fail(500, "Failed to update repair activity type.");
            }

            var oldType = await _repairActivityTypeRepository.GetRepairActivityTypeByIdAsync(repairActivityTypeId);
            if (oldType == null)
                return Result.Fail(404, "Original repair activity type not found.");

            var newType = new RepairActivityType
            {
                RepairActivityTypeId = repairActivityTypeDTO.RepairActivityTypeId,
                Name = repairActivityTypeDTO.Name
            };
            var addResult = await _repairActivityTypeRepository.AddRepairActivityTypeAsync(newType);
            if (!addResult)
                return Result.Fail(500, "Failed to add new repair activity type.");

            var repairActivities = _context.RepairActivities.Where(ra => ra.RepairActivityTypeId == repairActivityTypeId).ToList();
            foreach (var obj in repairActivities)
            {
                obj.RepairActivityTypeId = newType.RepairActivityTypeId;
            }
            await _context.SaveChangesAsync();

            await _repairActivityTypeRepository.DeleteRepairActivityTypeAsync(repairActivityTypeId);
            return Result.Ok("Repair activity type updated and replaced successfully.");
        }

        public async Task<Result> DeleteRepairActivityTypeAsync(string repairActivityTypeId)
        {
            var success = await _repairActivityTypeRepository.DeleteRepairActivityTypeAsync(repairActivityTypeId);
            return success
                ? Result.Ok("Repair activity type deleted successfully.")
                : Result.Fail(404, "Repair activity type not found.");
        }
    }
}
