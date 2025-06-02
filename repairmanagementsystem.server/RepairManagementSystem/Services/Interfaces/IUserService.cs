using RepairManagementSystem.Models.DTOs;
using RepairManagementSystem.Models;

namespace RepairManagementSystem.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO?> GetUserAsync(string email, string password);
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<bool> RegisterUserAsync(User user);
        Task<UserDTO?> GetUserByIdAsync(int userId);
        Task<User?> GetUserByEmailAsync(string email);
        Task<PasswordResetResponse> ResetPasswordAsync(int userId, PasswordResetRequest request);
        Task<UpdateUserInfoResponse> UpdateUserInfoAsync(int userId, UserInfoUpdateRequest request);
        Task<UpdateAddressResponse> UpdateAddressAsync(int userId, AddressUpdateRequest request);

    }
}
