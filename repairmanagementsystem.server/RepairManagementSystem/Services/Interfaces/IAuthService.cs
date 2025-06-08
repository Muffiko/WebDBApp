using Microsoft.AspNetCore.Http;
using RepairManagementSystem.Models.DTOs;
using RepairManagementSystem.Helpers;

namespace RepairManagementSystem.Services.Interfaces;

public interface IAuthService
{
    Task<Result<AuthResponse>> AuthenticateAsync(LoginRequest loginRequest);
    Task<Result<AuthResponse>> RegisterAsync(RegisterRequest registerRequest);
    Task<Result<AuthResponse>> RefreshTokenAsync(string refreshToken);
    int? GetUserIdFromToken(string token);
    Task DeleteRefreshTokenAsync(string refreshToken);
}
