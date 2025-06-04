namespace RepairManagementSystem.Models.DTOs
{
    public class UserMyInfoResponse
    {
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public AddressDTO? Address { get; set; }
        public DateTime LastLoginAt { get; set; }
        public DateTime AccountCreated { get; set; }
    }
}
