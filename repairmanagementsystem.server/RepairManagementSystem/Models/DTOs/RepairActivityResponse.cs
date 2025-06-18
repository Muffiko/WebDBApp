namespace RepairManagementSystem.Models.DTOs
{
    public class RepairActivityResponse
    {
        public int RepairActivityId { get; set; }

        public string Name { get; set; } = string.Empty;

        public RepairActivityType RepairActivityType { get; set; } = null!;

        public int SequenceNumber { get; set; }

        public string Description { get; set; } = string.Empty;

        public string Result { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public int WorkerId { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
