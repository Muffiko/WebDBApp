namespace RepairManagementSystem.Models.DTOs
{
    public class RepairRequestCustomerResponse
    {
        public int RequestId { get; set; }
        public string RepairObjectName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string? ManagerName { get; set; } = null;
    }
}  