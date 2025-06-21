namespace RepairManagementSystem.Models.DTOs
{
    public class ManagerDTO
    {
        public int ManagerId { get; set; }
        public string Expertise { get; set; } = string.Empty;
        public int ActiveRepairsCount { get; set; }
        public UserDTO User { get; set; } = null!;
    }
}