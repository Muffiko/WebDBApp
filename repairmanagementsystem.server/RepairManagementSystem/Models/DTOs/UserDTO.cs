namespace RepairManagementSystem.Models.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string Role { get; set; } = String.Empty;
    }
}
