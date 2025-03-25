using RepairManagementSystem.Models.DTOs;

namespace RepairManagementSystem.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponse?> AuthenticateAsync(LoginRequest loginRequest);
}
