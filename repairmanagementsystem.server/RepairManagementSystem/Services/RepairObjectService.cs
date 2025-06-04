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
        public RepairObjectService(ApplicationDbContext context, IMapper mapper, IRepairObjectRepository repairObjectRepository, IRepairObjectTypeRepository repairObjectTypeRepository)
        {
            _context = context;
            _mapper = mapper;
            _repairObjectRepository = repairObjectRepository;
            _repairObjectTypeRepository = repairObjectTypeRepository;
        }
        public async Task<IEnumerable<RepairObject?>?> GetAllRepairObjectsAsync()
        {
            var repairObjects = await _repairObjectRepository.GetAllRepairObjectsAsync();
            if (repairObjects == null || !repairObjects.Any())
                return null;
            return _mapper.Map<IEnumerable<RepairObject?>?>(repairObjects);
        }
        public async Task<RepairObject?> GetRepairObjectByIdAsync(int repairObjectId)
        {
            var repairObject = await _repairObjectRepository.GetRepairObjectByIdAsync(repairObjectId);
            if (repairObject == null)
                return null;
            return _mapper.Map<RepairObject?>(repairObject);
        }
        public async Task<bool> AddRepairObjectAsync(int userId, RepairObjectRequest repairObjectAddRequest)
        {
            if (repairObjectAddRequest == null)
                return false;
            var repairObject = _mapper.Map<RepairObject>(repairObjectAddRequest);
            repairObject.CustomerId = userId;
            return await _repairObjectRepository.AddRepairObjectAsync(repairObject);
        }
        public async Task<bool> UpdateRepairObjectAsync(int repairObjectId, RepairObjectRequest repairObjectRequest)
        {
            var updated = _mapper.Map<RepairObject>(repairObjectRequest);
            updated.RepairObjectId = repairObjectId;
            return await _repairObjectRepository.UpdateRepairObjectAsync(updated);
        }
        public async Task<bool> DeleteRepairObjectAsync(int repairObjectId)
        {
            return await _repairObjectRepository.DeleteRepairObjectAsync(repairObjectId);
        }
        public async Task<IEnumerable<RepairObject?>?> GetAllRepairObjectsFromCustomerAsync(int customerId)
        {
            var repairObjects = await _repairObjectRepository.GetAllRepairObjectsFromCustomerAsync(customerId);
            return repairObjects;
        }
    }
}
