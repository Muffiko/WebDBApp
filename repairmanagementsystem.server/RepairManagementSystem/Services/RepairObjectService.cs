using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RepairManagementSystem.Data;
using RepairManagementSystem.Models;
using RepairManagementSystem.Models.DTOs;
using RepairManagementSystem.Repositories;
using RepairManagementSystem.Repositories.Interfaces;
using RepairManagementSystem.Services.Interfaces;

namespace RepairManagementSystem.Services
{
    public class RepairObjectService : IRepairObjectService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IRepairObjectRepository _repairObjectRepository;
        private readonly IRepairObjectTypeRepository _repairObjectTypeRepository;

        public RepairObjectService(ApplicationDbContext context, IMapper mapper, IRepairObjectRepository repairObjectRepository)
        {
            _context = context;
            _mapper = mapper;
            _repairObjectRepository = repairObjectRepository;
        }

        public async Task<IEnumerable<RepairObject>> GetAllRepairObjectsAsync()
        {
            var repairObjects = await _repairObjectRepository.GetAllRepairObjectsAsync();
            return _mapper.Map<IEnumerable<RepairObject>>(repairObjects);
        }

        public async Task<RepairObject?> GetRepairObjectByIdAsync(int repairObjectId)
        {
            var repairObject = await _repairObjectRepository.GetRepairObjectByIdAsync(repairObjectId);
            return _mapper.Map<RepairObject>(repairObject);
        }

        public async Task<RepairObject?> AddRepairObjectAsync(RepairObjectDTO repairObjectDTO)
        {
            if (repairObjectDTO == null)
                return null;
            var repairObject = _mapper.Map<RepairObject>(repairObjectDTO);
            await _repairObjectRepository.AddRepairObjectAsync(repairObject);
            return repairObject;
        }

        public async Task<RepairObject?> UpdateRepairObjectAsync(int repairObjectId, RepairObjectDTO repairObjectDTO)
        {
            var updatedRepairObject = await _repairObjectRepository.GetRepairObjectByIdAsync(repairObjectId);
            if (updatedRepairObject == null)
                return null;
            await _repairObjectRepository.UpdateRepairObjectAsync(_mapper.Map(repairObjectDTO, updatedRepairObject));
            return _mapper.Map<RepairObject>(updatedRepairObject);
        }

        public async Task<RepairObject?> DeleteRepairObjectAsync(int repairObjectId)
        {
            var existingRepairObject = await _repairObjectRepository.GetRepairObjectByIdAsync(repairObjectId);
            if (existingRepairObject == null)
                return null;
            await _repairObjectRepository.DeleteRepairObjectAsync(repairObjectId);
            return _mapper.Map<RepairObject>(existingRepairObject);
        }

        public async Task<RepairObject?> AddRepairObjectAsync(RepairObjectAddDTO repairObjectAddDTO)
        {
            if (repairObjectAddDTO == null)
                return null;
            var repairObject = _mapper.Map<RepairObject>(repairObjectAddDTO);
            await _repairObjectRepository.AddRepairObjectAsync(repairObject);
            return repairObject;
        }

        public async Task<IEnumerable<RepairObject>> GetAllRepairObjectsFromCustomerAsync(int customerId)
        {
            var repairObjects = await _repairObjectRepository.GetAllRepairObjectsFromCustomerAsync(customerId);
            return repairObjects;
        }
    }
}
