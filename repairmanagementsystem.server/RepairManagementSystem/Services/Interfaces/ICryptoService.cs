using RepairManagementSystem.Models.DTOs;

namespace RepairManagementSystem.Services.Interfaces;

public interface ICryptoService
{
    (string Hash, string Salt) HashPassword(string password);
    bool VerifyPassword(string password, string hashedPassword, string salt);
}
