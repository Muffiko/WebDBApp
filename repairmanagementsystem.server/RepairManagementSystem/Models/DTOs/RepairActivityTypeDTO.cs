using System.ComponentModel.DataAnnotations;

namespace RepairManagementSystem.Models.DTOs
{
    public class RepairActivityTypeDTO
    {
        [Required]
        [MaxLength(64)]
        public string Name { get; set; } = string.Empty;
    }
}
