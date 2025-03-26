namespace RepairManagementSystem.Models.DTOs
{
    public class RepairActivityDTO
    {
        public int RepairActivityId { get; set; }
        public int RepairActivityTypeId { get; set; }
        public RepairActivityTypeDTO RepairActivityTypeDTO { get; set; }
        public string SequenceNumber { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Result { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int RepairTaskId { get; set; }
        public RepairTaskDTO RepairTaskDTO { get; set; }
        public int WorkerId { get; set; }
        public WorkerDTO WorkerDTO { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime FinishedAt { get; set; }
    }
}
