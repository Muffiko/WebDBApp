using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RepairManagementSystem.Models.DTOs
{
    public class RepairRequestDTO
    {
        public int RepairRequestId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int RepairObjectTypeId { get; set; }
        public RepairObjectTypeDTO RepairObjectTypeDTO { get; set; }
        public int CustomerId { get; set; }
        public CustomerDTO CustomerDTO { get; set; }
        public bool IsPaid { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime FinishedAt { get; set; }
        public ICollection<RepairTaskDTO> RepairTaskDTO { get; set; } = new List<RepairTaskDTO>();
    }
}
