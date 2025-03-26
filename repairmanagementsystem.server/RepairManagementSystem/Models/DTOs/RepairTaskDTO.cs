namespace RepairManagementSystem.Models.DTOs
{
    public class RepairTaskDTO
    {
        public int RepairTaskId { get; set; }
        public string Description { get; set; } = String.Empty;
        public string Result { get; set; } = String.Empty;
        public string Status { get; set; } = String.Empty;
        public int RepairRequestId { get; set; }
        public RepairRequestDTO RepairRequestDTO { get; set; }
        public int ManagerId { get; set; }
        public ManagerDTO ManagerDTO { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime FinishedAt { get; set; }
        public ICollection<RepairActivityDTO> RepairsActivitiesDTO { get; set; } = new List<RepairActivityDTO>();
    }
}
