namespace RepairManagementSystem.Models.DTOs
{
    public class UnassignedRepairRequest
    {
        public int RepairRequestId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string RepairObjectName { get; set; }
    }
}
