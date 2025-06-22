using System.ComponentModel.DataAnnotations;

namespace RepairManagementSystem.Models.DTOs
{
    public class RepairRequestAdd
    {
        [Required]
        [MaxLength(256)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public int RepairObjectId { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}
