namespace RepairManagementSystem.Models.DTOs
{
    public class ManagerDTO : UserDTO
    {
        public string Expertise { get; set; } = string.Empty;
        public int ActiveRepairsCount { get; set; }
        public ICollection<RepairTaskDTO> RepairsTasksDTO { get; set; } = new List<RepairTaskDTO>();
    }
}
