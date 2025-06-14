using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RepairManagementSystem.Data;
using RepairManagementSystem.Helpers;
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
            return _mapper.Map<IEnumerable<RepairObjectResponse?>?>(repairObjects);
        }

        public async Task<RepairObjectResponse?> GetRepairObjectByIdAsync(int repairObjectId)
        {
            var repairObject = await _repairObjectRepository.GetRepairObjectByIdAsync(repairObjectId);
            return _mapper.Map<RepairObjectResponse?>(repairObject);
        }

        public async Task<Result> AddRepairObjectAsync(int userId, RepairObjectRequest repairObjectAddRequest)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(userId);
            if (customer == null)
            {
                return Result.Fail(404, "Customer not found.");
            }

            var repairObjectType = await _repairObjectTypeRepository.GetRepairObjectTypeByIdAsync(repairObjectAddRequest.RepairObjectTypeId);
            if (repairObjectType == null)
            {
                return Result.Fail(404, "Repair object type not found.");
            }

            var repairObject = _mapper.Map<RepairObject>(repairObjectAddRequest);
            repairObject.CustomerId = userId;
            repairObject.Customer = customer;
            repairObject.RepairObjectType = repairObjectType;

            var success = await _repairObjectRepository.AddRepairObjectAsync(repairObject);
            return success
                ? Result.Ok("Repair object added successfully.")
                : Result.Fail(500, "Failed to add repair object.");
        }

        public async Task<Result> UpdateRepairObjectAsync(int repairObjectId, RepairObjectRequest repairObjectRequest)
        {
            var repairObjectType = await _repairObjectTypeRepository.GetRepairObjectTypeByIdAsync(repairObjectRequest.RepairObjectTypeId);
            if (repairObjectType == null)
            {
                return Result.Fail(404, "Repair object type not found.");
            }

            var updated = _mapper.Map<RepairObject>(repairObjectRequest);
            updated.RepairObjectId = repairObjectId;
            updated.RepairObjectType = repairObjectType;

            var success = await _repairObjectRepository.UpdateRepairObjectAsync(updated);
            return success
                ? Result.Ok("Repair object updated successfully.")
                : Result.Fail(500, "Failed to update repair object.");
        }

        public async Task<Result> DeleteRepairObjectAsync(int repairObjectId)
        {
            if (await _repairObjectRepository.DeleteRepairObjectAsync(repairObjectId))
            {
                return Result.Ok("Repair object deleted successfully.");
            }
            return Result.Fail(404, "Repair object not found.");
        }

        public async Task<IEnumerable<RepairObject?>?> GetAllRepairObjectsFromCustomerAsync(int customerId)
        {
            var repairObjects = await _repairObjectRepository.GetAllRepairObjectsFromCustomerAsync(customerId);
            return repairObjects;
        }
    }
}
