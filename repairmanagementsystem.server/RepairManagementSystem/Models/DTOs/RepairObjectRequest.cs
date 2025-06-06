using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepairManagementSystem.Models.DTOs
{
    public class RepairObjectRequest
    {
        [Required(ErrorMessage = "Repair Object Name is required.")]
        [MaxLength(64)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Repair Object Type ID is required.")]
        public string RepairObjectTypeId { get; set; } = string.Empty;

    }

}
