using Microsoft.AspNetCore.Http;
using RepairManagementSystem.Models.DTOs;
using RepairManagementSystem.Models;
using RepairManagementSystem.Helpers;

namespace RepairManagementSystem.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO?> GetUserAsync(string email, string password);
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<Result> RegisterUserAsync(User user);
        Task<UserDTO?> GetUserByIdAsync(int userId);
        Task<User?> GetUserByEmailAsync(string email);
        Task<Result> ResetPasswordAsync(int userId, PasswordResetRequest request);
        Task<Result<UpdateUserInfoResponse>> UpdateUserInfoAsync(int userId, UserInfoUpdateRequest request);
        Task<Result<UpdateAddressResponse>> UpdateAddressAsync(int userId, UpdateAddressRequest request);
        Task<User?> GetUserEntityByIdAsync(int userId);
        Task<Result> UpdateUserAsync(User user);
    }
}
