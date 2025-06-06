namespace RepairManagementSystem.Models.DTOs;

public class RefreshTokenResponse
{
    public string Token { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime ValidUntil { get; set; }
}
