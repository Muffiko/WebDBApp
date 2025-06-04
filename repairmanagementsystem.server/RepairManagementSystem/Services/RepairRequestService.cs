using AutoMapper;
using RepairManagementSystem.Data;
using RepairManagementSystem.Models;
using RepairManagementSystem.Models.DTOs;
using RepairManagementSystem.Repositories;
using RepairManagementSystem.Repositories.Interfaces;
using RepairManagementSystem.Services.Interfaces;

namespace RepairManagementSystem.Services
{
    public class RepairRequestService : IRepairRequestService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IRepairRequestRepository _repairRequestRepository;

        public RepairRequestService(ApplicationDbContext context, IMapper mapper, IRepairRequestRepository repairRequestRepository)
        {
            _context = context;
            _mapper = mapper;
            _repairRequestRepository = repairRequestRepository;
        }

        public async Task<IEnumerable<RepairRequest>> GetAllRepairRequestsAsync()
        {
            var repairRequests = await _repairRequestRepository.GetAllRepairRequestsAsync();
            return _mapper.Map<IEnumerable<RepairRequest>>(repairRequests);
        }

        public async Task<RepairRequest?> GetRepairRequestByIdAsync(int repairRequestId)
        {
            var repairRequest = await _repairRequestRepository.GetRepairRequestByIdAsync(repairRequestId);
            return _mapper.Map<RepairRequest>(repairRequest);
        }

        public async Task<RepairRequest?> AddRepairRequestAsync(RepairRequestDTO repairRequestDTO)
        {
            if (repairRequestDTO == null)
                return null;
            var repairRequest = _mapper.Map<RepairRequest>(repairRequestDTO);
            await _repairRequestRepository.AddRepairRequestAsync(repairRequest);
            return repairRequest;
        }

        public async Task<RepairRequest?> UpdateRepairRequestAsync(int repairRequestId, RepairRequestDTO repairRequestDTO)
        {
            var updatedRepairRequest = await _repairRequestRepository.GetRepairRequestByIdAsync(repairRequestId);
            if (updatedRepairRequest == null)
                return null;
            await _repairRequestRepository.UpdateRepairRequestAsync(_mapper.Map(repairRequestDTO, updatedRepairRequest));
            return _mapper.Map<RepairRequest>(updatedRepairRequest);
        }

        public async Task<RepairRequest?> DeleteRepairRequestAsync(int repairRequestId)
        {
            var existingRepairRequest = await _repairRequestRepository.GetRepairRequestByIdAsync(repairRequestId);
            if (existingRepairRequest == null)
                return null;
            await _repairRequestRepository.DeleteRepairRequestAsync(repairRequestId);
            return _mapper.Map<RepairRequest>(existingRepairRequest);
        }

        public async Task<RepairRequest?> AddRepairRequestAsync(RepairRequestAddDTO repairRequestAddDTO)
        {
            if (repairRequestAddDTO == null)
                return null;
            var repairRequest = _mapper.Map<RepairRequest>(repairRequestAddDTO);
            await _repairRequestRepository.AddRepairRequestAsync(repairRequest);
            return repairRequest;
        }

        public async Task<IEnumerable<RepairRequest>> GetAllRepairRequestsFromCustomerAsync(int customerId)
        {
            var repairRequests = await _repairRequestRepository.GetAllRepairObjectsFromCustomerAsync(customerId);
            return repairRequests;
        }
    }
}
