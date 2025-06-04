using AutoMapper;
using RepairManagementSystem.Data;
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

        public async Task<bool> AddRepairObjectTypeAsync(RepairObjectTypeDTO repairObjectTypeDTO)
        {
            if (repairObjectTypeDTO == null)
                return false;
            var repairObjectType = _mapper.Map<RepairObjectType>(repairObjectTypeDTO);
            return await _repairObjectTypeRepository.AddRepairObjectTypeAsync(repairObjectType);
        }

        public async Task<bool> UpdateRepairObjectTypeAsync(string repairObjectTypeId, RepairObjectTypeDTO repairObjectTypeDTO)
        {
            var updated = _mapper.Map<RepairObjectType?>(repairObjectTypeDTO);
            if (updated == null)
                return false;
            updated.RepairObjectTypeId = repairObjectTypeId;
            return await _repairObjectTypeRepository.UpdateRepairObjectTypeAsync(updated);
        }

        public async Task<bool> DeleteRepairObjectTypeAsync(string repairObjectTypeId)
        {
            return await _repairObjectTypeRepository.DeleteRepairObjectTypeAsync(repairObjectTypeId);
        }
        public async Task<IEnumerable<RepairObjectTypeDTO?>?> GetAllRepairObjectNameAsync()
        {
            var repairObjectTypes = await _repairObjectTypeRepository.GetAllRepairObjectTypesAsync();
            return _mapper.Map<IEnumerable<RepairObjectTypeDTO?>?>(repairObjectTypes);
        }
    }
}
