namespace RepairManagementSystem.Models.DTOs
{
    public class RepairActivityResponse
    {
        public int RepairActivityId { get; set; }

        public RepairActivityType RepairActivityType { get; set; } = null!;

        public string SequenceNumber { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Result { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public int WorkerId { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
