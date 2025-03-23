namespace RepairManagementSystem.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = String.Empty;
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public string Address { get; set; } = String.Empty;
        public string Number { get; set; } = String.Empty;
        public string Role { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
    }
}
