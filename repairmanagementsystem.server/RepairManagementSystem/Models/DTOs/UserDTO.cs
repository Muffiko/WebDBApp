namespace RepairManagementSystem.Models.DTOs
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty; // Required?
        public string Role { get; set; } = String.Empty;
        public string Number { get; set; } = String.Empty; // Required?
        public Address Address { get; set; } = new Address(); // Required?
        public DateTime CreatedAt { get; set; }
        public DateTime LastLoginAt { get; set; }
    }
}
