using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Identity.Client;
using RepairManagementSystem.Data;
using RepairManagementSystem.Helpers;
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
        private readonly ICustomerRepository _customerRepository;

        public UserService(ApplicationDbContext context, IMapper mapper, IUserRepository userRepository, ICryptoService cryptoService, ICustomerRepository customerRepository)
        {
            _context = context;
            _mapper = mapper;
            _userRepository = userRepository;
            _cryptoService = cryptoService;
            _customerRepository = customerRepository;
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
        public async Task<Result> RegisterUserAsync(User user)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(user.Email);
            if (existingUser != null)
            {
                return Result.Fail(409, "A user with this email already exists.");
            }

            var result = await _userRepository.AddUserAsync(user);
            if (!result)
            {
                return Result.Fail(500, "Failed to add user to the database.");
            }

            var customer = new Customer { UserId = user.UserId, PaymentMethod = string.Empty, User = user };
            if (await _customerRepository.AddCustomerAsync(customer))
            {
                return Result.Ok("User registered successfully.");
            }
            return Result.Fail(500, "Failed to add customer information.");
        }
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);

        }
        public async Task<Result> ResetPasswordAsync(int userId, PasswordResetRequest request)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                return Result.Fail(404, "User not found.");
            }
            if (!_cryptoService.VerifyPassword(request.OldPassword, user.PasswordHash, user.PasswordSalt))
            {
                return Result.Fail(400, "Old password is incorrect.");
            }
            var (newHash, newSalt) = _cryptoService.HashPassword(request.NewPassword);
            user.PasswordHash = newHash;
            user.PasswordSalt = newSalt;
            if (await _userRepository.UpdateUserAsync(user))
            {
                return Result.Ok("Password reset successfully.");
            }
            return Result.Fail(500, "Failed to reset password. Please try again.");
        }

        public async Task<Result<UpdateUserInfoResponse>> UpdateUserInfoAsync(int userId, UserInfoUpdateRequest request)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                return Result<UpdateUserInfoResponse>.Fail(404, "User not found.");
            }
            var updatedFields = new List<string>();
            if (request.FirstName != null)
            {
                user.FirstName = request.FirstName;
                updatedFields.Add("first name");
            }
            if (request.LastName != null)
            {
                user.LastName = request.LastName;
                updatedFields.Add("last name");
            }
            if (request.Email != null)
            {
                user.Email = request.Email;
                updatedFields.Add("email");
            }
            if (request.PhoneNumber != null)
            {
                user.Number = request.PhoneNumber;
                updatedFields.Add("phone number");
            }
            if (updatedFields.Count == 0)
            {
                return Result<UpdateUserInfoResponse>.Fail(400, "No fields were provided to update.");
            }
            if (await _userRepository.UpdateUserAsync(user))
            {
                var updatedList = string.Join(", ", updatedFields);
                return Result<UpdateUserInfoResponse>.Ok(new UpdateUserInfoResponse { Success = true, Message = $"Updated {updatedList}." });
            }
            return Result<UpdateUserInfoResponse>.Fail(500, "Failed to update user information. Please try again.");
        }

        public async Task<Result<UpdateAddressResponse>> UpdateAddressAsync(int userId, UpdateAddressRequest request)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                return Result<UpdateAddressResponse>.Fail(404, "User not found.");
            }
            var updatedFields = new List<string>();
            if (request.Street != null)
            {
                user.Address.Street = request.Street;
                updatedFields.Add("street");
            }
            if (request.City != null)
            {
                user.Address.City = request.City;
                updatedFields.Add("city");
            }
            if (request.PostalCode != null)
            {
                user.Address.PostalCode = request.PostalCode;
                updatedFields.Add("postal code");
            }
            if (request.Country != null)
            {
                user.Address.Country = request.Country;
                updatedFields.Add("country");
            }
            if (request.ApartNumber != null)
            {
                user.Address.ApartNumber = request.ApartNumber;
                updatedFields.Add("apartment number");
            }
            if (request.HouseNumber != null)
            {
                user.Address.HouseNumber = request.HouseNumber;
                updatedFields.Add("house number");
            }
            if (updatedFields.Count == 0)
            {
                return Result<UpdateAddressResponse>.Fail(400, "No address fields were provided to update.");
            }
            if (await _userRepository.UpdateUserAsync(user))
            {
                var updatedList = string.Join(", ", updatedFields);
                return Result<UpdateAddressResponse>.Ok(new UpdateAddressResponse { Success = true, Message = $"Updated address fields: {updatedList}." });
            }
            return Result<UpdateAddressResponse>.Fail(500, "Failed to update address. Please try again.");
        }

        public async Task<User?> GetUserEntityByIdAsync(int userId)
        {
            return await _userRepository.GetUserByIdAsync(userId);
        }

        public async Task<Result> UpdateUserAsync(User user)
        {
            if (user == null)
            {
                return Result.Fail();
            }
            return await _userRepository.UpdateUserAsync(user)
                ? Result.Ok("User updated successfully.")
                : Result.Fail(500, "Failed to update user.");
        }
    }
}
