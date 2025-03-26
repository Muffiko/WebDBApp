namespace RepairManagementSystem.Models.DTOs
{
    public class RepairObjectTypeDTO
    {
        public int RepairObjectTypeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<RepairRequestDTO> RepairsRequestsDTO { get; set; } = new List<RepairRequestDTO>();
    }
}
