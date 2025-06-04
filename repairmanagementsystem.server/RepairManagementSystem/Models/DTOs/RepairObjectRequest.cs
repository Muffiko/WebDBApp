using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepairManagementSystem.Models.DTOs
{
    public class RepairObjectRequest
    {
        [Required]
        [MaxLength(64)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string RepairObjectTypeId { get; set; } = string.Empty;

    }

}
