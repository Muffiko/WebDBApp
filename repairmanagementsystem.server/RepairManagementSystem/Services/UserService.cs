using AutoMapper;
using RepairManagementSystem.Data;
using RepairManagementSystem.Models;
using RepairManagementSystem.Models.DTOs;
using RepairManagementSystem.Repositories.Interfaces;
using RepairManagementSystem.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

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

        public async Task RegisterUserAsync(UserCreateDTO userCreateDto)
        {
            var user = _mapper.Map<User>(userCreateDto);
            user.PasswordHash = HashPassword(userCreateDto.Password);
            user.CreatedAt = DateTime.UtcNow;
            user.LastLoginAt = DateTime.UtcNow;
            user.IsActive = true;
            user.Role = "User";
            await _userRepository.AddUserAsync(user);
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
