using AutoMapper;
using Microsoft.Identity.Client;
using RepairManagementSystem.Data;
using RepairManagementSystem.Models;
using RepairManagementSystem.Models.DTOs;
using RepairManagementSystem.Repositories.Interfaces;
using RepairManagementSystem.Services.Interfaces;

namespace RepairManagementSystem.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ICryptoService _cryptoService;

        public UserService(ApplicationDbContext context, IMapper mapper, IUserRepository userRepository, ICryptoService cryptoService)
        {
            _context = context;
            _mapper = mapper;
            _userRepository = userRepository;
            _cryptoService = cryptoService;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<UserDTO?> GetUserAsync(string email, string passwordHash)
        {
            var user = await _userRepository.GetUserAsync(email, passwordHash);
            return _mapper.Map<UserDTO?>(user);
        }

        public async Task<UserDTO?> GetUserByIdAsync(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            return _mapper.Map<UserDTO?>(user);
        }
        public async Task<bool> RegisterUserAsync(User user)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(user.Email);
            if (existingUser != null)
            {
                return false;
            }

            return await _userRepository.AddUserAsync(user);
        }
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);

        }
        public async Task<PasswordResetResponse> ResetPasswordAsync(int userId, PasswordResetRequest request)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                return new PasswordResetResponse { Success = false, Message = "Failed to reset password. Please try again." };
            }

            if (!_cryptoService.VerifyPassword(request.OldPassword, user.PasswordHash, user.PasswordSalt))
            {
                return new PasswordResetResponse { Success = false, Message = "Old password is incorrect." };
            }

            var (newHash, newSalt) = _cryptoService.HashPassword(request.NewPassword);
            user.PasswordHash = newHash;
            user.PasswordSalt = newSalt;

            if (await _userRepository.UpdateUserAsync(user))
            {
                return new PasswordResetResponse { Success = true, Message = "Password reset successfully." };
            }

            return new PasswordResetResponse { Success = false, Message = "Failed to reset password. Please try again." };
        }
    }
}
