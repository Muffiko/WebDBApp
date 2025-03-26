namespace RepairManagementSystem.Models.DTOs
{
    public class WorkerDTO : UserDTO
    {
        public string Expertise { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
        public ICollection<RepairActivityDTO> RepairsActivitiesDTO { get; set; } = new List<RepairActivityDTO>();
    }
}
