using AutoMapper;
using RepairManagementSystem.Data;
using RepairManagementSystem.Helpers;
using RepairManagementSystem.Models;
using RepairManagementSystem.Models.DTOs;
using RepairManagementSystem.Repositories.Interfaces;
using RepairManagementSystem.Services.Interfaces;

namespace RepairManagementSystem.Services
{
    public class RepairObjectTypeService : IRepairObjectTypeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IRepairObjectTypeRepository _repairObjectTypeRepository;

        public RepairObjectTypeService(ApplicationDbContext context, IMapper mapper, IRepairObjectTypeRepository repairObjectTypeRepository)
        {
            _context = context;
            _mapper = mapper;
            _repairObjectTypeRepository = repairObjectTypeRepository;
        }

        public async Task<IEnumerable<RepairObjectType?>?> GetAllRepairObjectTypesAsync()
        {
            var repairObjectTypes = await _repairObjectTypeRepository.GetAllRepairObjectTypesAsync();
            return _mapper.Map<IEnumerable<RepairObjectType?>?>(repairObjectTypes);
        }

        public async Task<RepairObjectType?> GetRepairObjectTypeByIdAsync(string repairObjectTypeId)
        {
            var repairObjectType = await _repairObjectTypeRepository.GetRepairObjectTypeByIdAsync(repairObjectTypeId);
            return _mapper.Map<RepairObjectType?>(repairObjectType);
        }

        public async Task<Result> AddRepairObjectTypeAsync(RepairObjectTypeDTO repairObjectTypeDTO)
        {
            if (repairObjectTypeDTO == null)
                return Result.Fail(400, "Invalid repair object type data.");
            var repairObjectType = _mapper.Map<RepairObjectType>(repairObjectTypeDTO);
            var success = await _repairObjectTypeRepository.AddRepairObjectTypeAsync(repairObjectType);
            return success
                ? Result.Ok("Repair object type added successfully.")
                : Result.Fail(500, "Failed to add repair object type.");
        }

        public async Task<Result> UpdateRepairObjectTypeAsync(string repairObjectTypeId, RepairObjectTypeDTO repairObjectTypeDTO)
        {
            if (repairObjectTypeId == repairObjectTypeDTO.RepairObjectTypeId)
            {
                var updated = _mapper.Map<RepairObjectType?>(repairObjectTypeDTO);
                if (updated == null)
                    return Result.Fail(400, "Invalid repair object type data.");
                updated.RepairObjectTypeId = repairObjectTypeId;
                var success = await _repairObjectTypeRepository.UpdateRepairObjectTypeAsync(updated);
                return success
                    ? Result.Ok("Repair object type updated successfully.")
                    : Result.Fail(500, "Failed to update repair object type.");
            }

            var oldType = await _repairObjectTypeRepository.GetRepairObjectTypeByIdAsync(repairObjectTypeId);
            if (oldType == null)
                return Result.Fail(404, "Original repair object type not found.");

            var newType = new RepairObjectType
            {
                RepairObjectTypeId = repairObjectTypeDTO.RepairObjectTypeId,
                Name = repairObjectTypeDTO.Name
            };
            var addResult = await _repairObjectTypeRepository.AddRepairObjectTypeAsync(newType);
            if (!addResult)
                return Result.Fail(500, "Failed to add new repair object type.");

            var repairObjects = _context.RepairObjects.Where(ro => ro.RepairObjectTypeId == repairObjectTypeId).ToList();
            foreach (var obj in repairObjects)
            {
                obj.RepairObjectTypeId = newType.RepairObjectTypeId;
            }
            await _context.SaveChangesAsync();

            await _repairObjectTypeRepository.DeleteRepairObjectTypeAsync(repairObjectTypeId);
            return Result.Ok("Repair object type updated and replaced successfully.");
        }

        public async Task<Result> DeleteRepairObjectTypeAsync(string repairObjectTypeId)
        {
            var success = await _repairObjectTypeRepository.DeleteRepairObjectTypeAsync(repairObjectTypeId);
            return success
                ? Result.Ok("Repair object type deleted successfully.")
                : Result.Fail(404, "Repair object type not found.");
        }
        public async Task<IEnumerable<RepairObjectTypeDTO?>?> GetAllRepairObjectNameAsync()
        {
            var repairObjectTypes = await _repairObjectTypeRepository.GetAllRepairObjectTypesAsync();
            return _mapper.Map<IEnumerable<RepairObjectTypeDTO?>?>(repairObjectTypes);
        }
    }
}
