using AutoMapper;
using RepairManagementSystem.Data;
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

        public async Task<bool> AddRepairActivityTypeAsync(RepairActivityTypeDTO repairActivityTypeDTO)
        {
            if (repairActivityTypeDTO == null)
                return false;
            var repairActivityType = _mapper.Map<RepairActivityType>(repairActivityTypeDTO);
            return await _repairActivityTypeRepository.AddRepairActivityTypeAsync(repairActivityType);
        }

        public async Task<bool> UpdateRepairActivityTypeAsync(string repairActivityTypeId, RepairActivityTypeDTO repairActivityTypeDTO)
        {
            var updated = _mapper.Map<RepairActivityType?>(repairActivityTypeDTO);
            if (updated == null)
                return false;
            updated.RepairActivityTypeId = repairActivityTypeId;
            return await _repairActivityTypeRepository.UpdateRepairActivityTypeAsync(updated);
        }

        public async Task<bool> DeleteRepairActivityTypeAsync(string repairActivityTypeId)
        {
            return await _repairActivityTypeRepository.DeleteRepairActivityTypeAsync(repairActivityTypeId);
        }
    }
}
