using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepairManagementSystem.Models.DTOs
{
    public class RepairRequestDTO
    {
        [Required]
        [MaxLength(256)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [MaxLength(32)]
        public string Result { get; set; } = string.Empty;

        [Required]
        [MaxLength(32)]
        public string Status { get; set; } = string.Empty;

        [Required]
        public int RepairObjectId { get; set; }

        [Required]
        public int ManagerId { get; set; }

        [Required]
        public bool IsPaid { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime StartedAt { get; set; }

        [Required]
        public DateTime FinishedAt { get; set; }
    }
}
