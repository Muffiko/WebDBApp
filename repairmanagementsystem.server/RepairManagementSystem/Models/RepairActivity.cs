using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepairManagementSystem.Models
{
    [Table("RepairsActivities")]
    public class RepairActivity
    {
        [Key]
        public int RepairActivityId { get; set; }

        [Required]
        public string RepairActivityTypeId { get; set; } = string.Empty;

        [ForeignKey(nameof(RepairActivityTypeId))]
        public RepairActivityType RepairActivityType { get; set; } = null!;

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

        [ForeignKey(nameof(RepairRequestId))]
        public RepairRequest RepairRequest { get; set; } = null!;

        [Required]
        public int WorkerId { get; set; }

        [ForeignKey(nameof(WorkerId))]
        public Worker Worker { get; set; } = null!;

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime StartedAt { get; set; }

        [Required]
        public DateTime FinishedAt { get; set; }
    }
}
