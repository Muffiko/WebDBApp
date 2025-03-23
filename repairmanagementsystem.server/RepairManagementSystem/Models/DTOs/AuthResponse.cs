namespace RepairManagementSystem.Models.DTOs
{
    public class AuthResponse
    {
        public string Token { get; set; } = String.Empty;
        public string UserName { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string Role { get; set; } = String.Empty;
    }
}
