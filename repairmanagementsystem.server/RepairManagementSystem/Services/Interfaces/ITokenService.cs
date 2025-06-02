using RepairManagementSystem.Models.DTOs;

namespace RepairManagementSystem.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(UserDTO user);
        string GenerateRefreshToken();
        bool ValidateToken(string token);
        string HashToken(string token);
        Task<bool> RefreshTokenExists(string hashedRefreshToken);
        Task<int> GetUserIdByRefreshToken(string hashedRefreshToken);
        int? GetUserIdFromToken(string token);
    }
}
