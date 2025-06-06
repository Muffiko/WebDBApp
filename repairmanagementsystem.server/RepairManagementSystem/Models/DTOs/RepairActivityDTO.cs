using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepairManagementSystem.Models.DTOs
{
    public class RepairActivityDTO
    {
        [Required]
        public string RepairActivityTypeId { get; set; } = string.Empty;

        [Required]
        [MaxLength(16)]
        public string SequenceNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(256)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [MaxLength(256)]
        public string Result { get; set; } = string.Empty;

        [Required]
        [MaxLength(3)]
        public string Status { get; set; } = string.Empty;

        [Required]
        public int RepairRequestId { get; set; }

        [Required]
        public int WorkerId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime StartedAt { get; set; }

        [Required]
        public DateTime FinishedAt { get; set; }
    }
}
