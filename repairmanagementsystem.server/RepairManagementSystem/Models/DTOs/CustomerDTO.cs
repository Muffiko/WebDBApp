namespace RepairManagementSystem.Models.DTOs
{
    public class CustomerDTO
    {
        public int CustomerId { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public UserDTO User { get; set; } = null!;
    }
}