namespace RepairManagementSystem.Models.DTOs
{
    public class RepairActivityTypeDTO
    {
        public int RepairActivityTypeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<RepairActivityDTO> RepairsActivitiesDTO { get; set; } = new List<RepairActivityDTO>();
    }
}
