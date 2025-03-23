using RepairManagementSystem.Models.DTOs;

namespace RepairManagementSystem.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(UserDTO user);
        bool ValidateToken(string token);
    }
}
