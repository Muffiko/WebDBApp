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
            var repairActivityType = _mapper.Map<RepairActivityType>(repairActivityTypeDTO);
            var success = await _repairActivityTypeRepository.AddRepairActivityTypeAsync(repairActivityType);
            return success
                ? Result.Ok("Repair activity type added successfully.")
                : Result.Fail(500, "Failed to add repair activity type.");
        }

        public async Task<Result> UpdateRepairActivityTypeAsync(string repairActivityTypeId, RepairActivityTypeDTO repairActivityTypeDTO)
        {
            var updated = _mapper.Map<RepairActivityType?>(repairActivityTypeDTO);
            if (updated == null)
                return Result.Fail(400, "Repair activity type cannot be null or invalid.");
            updated.RepairActivityTypeId = repairActivityTypeId;
            var success = await _repairActivityTypeRepository.UpdateRepairActivityTypeAsync(updated);
            return success
                ? Result.Ok($"Repair activity type with ID {repairActivityTypeId} updated successfully.")
                : Result.Fail(404, $"Repair activity type with ID {repairActivityTypeId} not found.");
        }

        public async Task<Result> DeleteRepairActivityTypeAsync(string repairActivityTypeId)
        {
            var success = await _repairActivityTypeRepository.DeleteRepairActivityTypeAsync(repairActivityTypeId);
            return success
                ? Result.Ok($"Repair activity type with ID {repairActivityTypeId} deleted successfully.")
                : Result.Fail(404, $"Repair activity type with ID {repairActivityTypeId} not found.");
        }
    }
}
