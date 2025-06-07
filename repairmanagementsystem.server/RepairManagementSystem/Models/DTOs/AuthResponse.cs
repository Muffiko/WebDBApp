using System.Text.Json.Serialization;

namespace RepairManagementSystem.Models.DTOs
{
    public class AuthResponse
    {
        public string Token { get; set; } = string.Empty;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? RefreshToken { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
