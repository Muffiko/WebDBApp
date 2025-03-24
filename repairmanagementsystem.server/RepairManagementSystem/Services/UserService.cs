using RepairManagementSystem.Data;
using RepairManagementSystem.Models.DTOs;
using RepairManagementSystem.Services.Interfaces;
using RepairManagementSystem.Repositories.Interfaces;
using AutoMapper;

namespace RepairManagementSystem.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;


        public UserService(ApplicationDbContext context, IMapper mapper, IUserRepository userRepository)
        {
            _context = context;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<UserDTO?> GetUserAsync(string email, string password)
        {
            var user = await _userRepository.GetUserAsync(email, password);
            return _mapper.Map<UserDTO?>(user);
        }

    }
}
