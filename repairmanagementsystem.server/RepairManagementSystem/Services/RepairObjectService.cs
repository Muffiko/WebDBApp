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
        private readonly ICustomerRepository _customerRepository;

        public RepairObjectService(ApplicationDbContext context, IMapper mapper, IRepairObjectRepository repairObjectRepository, IRepairObjectTypeRepository repairObjectTypeRepository, ICustomerRepository customerRepository)
        {
            _context = context;
            _mapper = mapper;
            _repairObjectRepository = repairObjectRepository;
            _repairObjectTypeRepository = repairObjectTypeRepository;
            _customerRepository = customerRepository;
        }
        public async Task<IEnumerable<RepairObjectResponse?>?> GetAllRepairObjectsAsync()
        {
            var repairObjects = await _repairObjectRepository.GetAllRepairObjectsAsync();
            if (repairObjects == null || !repairObjects.Any())
                return null;
            return _mapper.Map<IEnumerable<RepairObjectResponse?>?>(repairObjects);
        }
        public async Task<RepairObjectResponse?> GetRepairObjectByIdAsync(int repairObjectId)
        {
            var repairObject = await _repairObjectRepository.GetRepairObjectByIdAsync(repairObjectId);
            if (repairObject == null)
                return null;
            return _mapper.Map<RepairObjectResponse?>(repairObject);
        }
        public async Task<bool> AddRepairObjectAsync(int userId, RepairObjectRequest repairObjectAddRequest)
        {
            var repairObject = _mapper.Map<RepairObject>(repairObjectAddRequest);
            repairObject.CustomerId = userId;
            var customer = await _customerRepository.GetCustomerByIdAsync(userId);
            if (customer == null)
                return false;
            repairObject.Customer = customer;
            var repairObjectType = await _repairObjectTypeRepository.GetRepairObjectTypeByIdAsync(repairObjectAddRequest.RepairObjectTypeId);
            if (repairObjectType == null)
                return false;
            repairObject.RepairObjectType = repairObjectType;
            return await _repairObjectRepository.AddRepairObjectAsync(repairObject);
        }
        public async Task<bool> UpdateRepairObjectAsync(int repairObjectId, RepairObjectRequest repairObjectRequest)
        {
            var repairObjectType = await _repairObjectTypeRepository.GetRepairObjectTypeByIdAsync(repairObjectRequest.RepairObjectTypeId);
            if (repairObjectType == null)
                return false;
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
