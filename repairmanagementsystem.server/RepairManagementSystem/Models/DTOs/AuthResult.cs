namespace RepairManagementSystem.Models.DTOs
{
    public class AuthResult
    {
        public bool Success { get; set; }
        public AuthResponse? Response { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
