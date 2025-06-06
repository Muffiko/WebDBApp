using System.ComponentModel.DataAnnotations;

namespace RepairManagementSystem.Models.DTOs
{
    public class RepairObjectTypeDTO
    {
        [Required(ErrorMessage = "Repair Object Type String ID is required.")]
        [MaxLength(16)]
        public string RepairObjectTypeId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Repair Object Type Name is required.")]
        [MaxLength(64)]
        public string Name { get; set; } = string.Empty;
    }
}
