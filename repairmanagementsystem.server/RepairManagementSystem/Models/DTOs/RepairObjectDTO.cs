using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepairManagementSystem.Models.DTOs
{
    public class RepairObjectDTO
    {
        [Required]
        [MaxLength(64)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string RepairObjectTypeId { get; set; }

        [Required]
        public int CustomerId { get; set; }
    }

    public class RepairObjectAddDTO
    {
        [Required]
        [MaxLength(64)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string RepairObjectTypeId { get; set; }
    }
}
