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

        public async Task<IEnumerable<RepairObjectType>> GetAllRepairObjectTypesAsync()
        {
            var repairObjectTypes = await _repairObjectTypeRepository.GetAllRepairObjectTypesAsync();
            return _mapper.Map<IEnumerable<RepairObjectType>>(repairObjectTypes);
        }

        public async Task<RepairObjectType?> GetRepairObjectTypeByIdAsync(string repairObjectTypeId)
        {
            var repairObjectType = await _repairObjectTypeRepository.GetRepairObjectTypeByIdAsync(repairObjectTypeId);
            return _mapper.Map<RepairObjectType>(repairObjectType);
        }

        public async Task<RepairObjectType?> AddRepairObjectTypeAsync(RepairObjectTypeDTO repairObjectTypeDTO)
        {
            if (repairObjectTypeDTO == null)
                return null;
            var repairObjectType = _mapper.Map<RepairObjectType>(repairObjectTypeDTO);
            await _repairObjectTypeRepository.AddRepairObjectTypeAsync(repairObjectType);
            return repairObjectType;
        }

        public async Task<RepairObjectType?> UpdateRepairObjectTypeAsync(string repairObjectTypeId, RepairObjectTypeDTO repairObjectTypeDTO)
        {
            var updatedRepairObjectType = await _repairObjectTypeRepository.GetRepairObjectTypeByIdAsync(repairObjectTypeId);
            if (updatedRepairObjectType == null)
                return null;
            await _repairObjectTypeRepository.UpdateRepairObjectTypeAsync(_mapper.Map(repairObjectTypeDTO, updatedRepairObjectType));
            return _mapper.Map<RepairObjectType>(updatedRepairObjectType);
        }

        public async Task<RepairObjectType?> DeleteRepairObjectTypeAsync(string repairObjectTypeId)
        {
            var existingRepairObjectType = await _repairObjectTypeRepository.GetRepairObjectTypeByIdAsync(repairObjectTypeId);
            if (existingRepairObjectType == null)
                return null;
            await _repairObjectTypeRepository.DeleteRepairObjectTypeAsync(repairObjectTypeId);
            return _mapper.Map<RepairObjectType>(existingRepairObjectType);
        }
        public async Task<IEnumerable<RepairObjectTypeDTO>> GetAllRepairObjectNameAsync()
        {
            var repairObjectTypes = await _repairObjectTypeRepository.GetAllRepairObjectTypesAsync();
            return _mapper.Map<IEnumerable<RepairObjectTypeDTO>>(repairObjectTypes);
        }
    }
}
