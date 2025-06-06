namespace RepairManagementSystem.Models.DTOs
{
    public class RepairRequestCustomerResponse
    {
        public int Request_Id { get; set; }
        public string Status { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public string? ManagerName { get; set; } = null;
    }
}  