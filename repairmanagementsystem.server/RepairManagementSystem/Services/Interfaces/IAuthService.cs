using RepairManagementSystem.Models.DTOs;

namespace RepairManagementSystem.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResult> AuthenticateAsync(LoginRequest loginRequest);
    Task<AuthResult> RegisterAsync(RegisterRequest registerRequest);
    Task<RefreshTokenResponse?> RefreshTokenAsync(string refreshToken);
    int? GetUserIdFromToken(string token);
    Task DeleteRefreshTokenAsync(string refreshToken);
}
