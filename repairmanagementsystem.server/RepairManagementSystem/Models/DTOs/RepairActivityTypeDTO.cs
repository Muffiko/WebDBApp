using System.ComponentModel.DataAnnotations;

namespace RepairManagementSystem.Models.DTOs
{
    public class RepairActivityTypeDTO
    {
        [Required(ErrorMessage = "Repair Activity Type String ID is required.")]
        [MaxLength(16)]
        public string RepairActivityTypeId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Repair Activity Type Name is required.")]
        [MaxLength(64)]
        public string Name { get; set; } = string.Empty;
    }
}
