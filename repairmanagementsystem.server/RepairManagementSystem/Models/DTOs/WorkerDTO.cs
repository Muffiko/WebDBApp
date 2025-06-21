namespace RepairManagementSystem.Models.DTOs
{
    public class WorkerDTO
    {
        public int WorkerId { get; set; }
        public bool IsAvailable { get; set; }
        public string Expertise { get; set; } = string.Empty;
        public UserDTO User { get; set; } = null!;
    }
}
